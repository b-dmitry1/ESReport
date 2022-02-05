using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SourceEditor;

namespace ESReport
{
	public partial class MainForm : Form
	{
		private ReportPreview _preview;
		private bool _timerLocked = false;
		private bool _changed = true;
		private string reportFileName = "report.txt";
		private string dataFileName = "data.txt";
		private Json _json = new Json();
		private SourceEditor.SourceEditor _editor;

		public MainForm()
		{
			InitializeComponent();

			_preview = new ReportPreview();
			_preview.Parent = PreviewPanel;
			_preview.Dock = DockStyle.Fill;

			_editor = new SourceEditor.SourceEditor();
			_editor.Parent = SourcePanel;
			_editor.Dock = DockStyle.Fill;

			_editor.Keywords = new string[] { "row", "cell", "style", "foreach" };
			_editor.Identifiers = new string[] { "text", "w", "h", "fontsize", "id",
				"align", "color", "data", "header", "frame",
				"margins", "leftmargin", "topmargin", "rightmargin", "bottommargin" };

			_editor.TextChanged += _editor_TextChanged;

		}

		void _editor_TextChanged(object sender, EventArgs e)
		{
			_changed = true;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			var cmd = Environment.GetCommandLineArgs();

			var collapse = cmd.Length > 1;

			var arg = 1;

			if (cmd.Length > arg)
			{
				if (cmd[arg] == "edit")
				{
					collapse = false;
					arg++;
				}
			}

			if (cmd.Length > arg)
			{
				if (File.Exists(cmd[arg]))
				{
					reportFileName = cmd[arg];
				}
			}

			arg++;

			if (cmd.Length > arg)
			{
				if (File.Exists(cmd[arg]))
				{
					dataFileName = cmd[arg];
				}
			}

			if (collapse)
			{
				splitContainer1.Panel1Collapsed = true;
			}

			if (File.Exists(reportFileName))
			{
				_editor.LoadFromFile(reportFileName);
			}

			_preview.Focus();

			timer1.Enabled = true;

			_changed = true;
		}

		void ReportTextBox_MouseWheel(object sender, MouseEventArgs e)
		{
			var r = ((Control)sender).ClientRectangle;

			if (e.X >= r.Left && e.Y >= r.Top && e.X <= r.Right && e.Y <= r.Bottom)
			{
				return;
			}

			_preview.ProcessMouseWheel(e.Delta);
			((HandledMouseEventArgs)e).Handled = true;
		}

		IEnumerable<IDictionary<string, string>> GetTable(string name)
		{
			var res = new List<Dictionary<string, string>>();

			var data = _json[name];

			foreach (var row in data.Items)
			{
				var d = new Dictionary<string, string>();
				
				foreach (var value in row.Items)
				{
					d[value.Name] = value.StrValue;
				}

				res.Add(d);
			}

			return res;
		}

		private async Task<Json> LoadJson(string json)
		{
			return await Task.Run(() =>
			{
				var markup = new MarkupParser();

				var res = new Json();
				try
				{
					res.LoadJson(json);
				}
				catch
				{
				}

				return res;
			});
		}

		private async Task<IReport> DoMarkup(string text, string json)
		{
			return await Task.Run(async () =>
			{
				var markup = new MarkupParser();

				IReport report = null;
				try
				{
					report = await markup.Parse(text, new ESReportScriptParser(), GetTable);
				}
				catch
				{
					throw;
				}

				return report;
			});
		}

		private void MarkSearchMatches(string search, IEnumerable<IPage> pages)
		{
			if (search.Length == 0)
			{
				return;
			}

			foreach (var page in pages)
			{ 
				foreach (var element in page.Elements)
				{
					if (element.Text.Contains(search))
					{
						element.Style.Color = Color.Yellow;
					}
				}
			}
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			if (!_changed)
			{
				return;
			}

			if (_timerLocked)
			{
				return;
			}

			_timerLocked = true;

			_changed = false;

			var data = "";
			if (File.Exists(dataFileName))
			{
				try
				{
					data = File.ReadAllText(dataFileName);
				}
				catch
				{ 
				}
			}

			await makeReport(_editor.Text, data, SearchTextBox.Text);

			_preview.Refresh();

			_timerLocked = false;
		}

		private async Task<string> makeReport(string text, string json, string search)
		{
			var result = "";

			IEnumerable<Metafile> metafiles = new List<Metafile>();

			IReport report = null;

			try
			{
				_json = await LoadJson(json);

				report = await DoMarkup(text, json);

				if (report != null)
				{
					var composer = new Composer();

					var measurer = new TextMeasurer();

					var pages = composer.Compose(report, measurer);

					MarkSearchMatches(search, pages);

					var mfr = new MetafileRenderer();

					metafiles = mfr.Render(pages);

					foreach (var mf in _preview.Pages)
					{
						mf.Dispose();
					}

					_preview.Pages = metafiles;
				}
				result = "OK";
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}

			return result;
		}

		private void ReportTextBox_TextChanged(object sender, EventArgs e)
		{
			_changed = true;
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			new PrintCommand(_preview.Pages)
				.Execute();

			_preview.Focus();
		}

		private void SearchTextBox_TextChanged(object sender, EventArgs e)
		{
			_changed = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new SaveCommand(reportFileName, _editor.Lines)
				.Execute();

			_preview.Focus();
		}
	}
}
