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

namespace ESReport
{
	public partial class MainForm : Form
	{
		private ReportPreview _preview;
		private bool _timerLocked = false;
		private bool _changed = true;
		private int _printingPage = 0;

		private Json _json = new Json();

		public MainForm()
		{
			InitializeComponent();

			_preview = new ReportPreview();
			_preview.Parent = PreviewPanel;
			_preview.Dock = DockStyle.Fill;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			var cmd = Environment.GetCommandLineArgs();

			var reportFileName = "report.txt";
			var dataFileName = "data.txt";

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
				ReportTextBox.Text = File.ReadAllText(reportFileName);
			}

			if (File.Exists(dataFileName))
			{
				DataTextBox.Text = File.ReadAllText(dataFileName);
			}

			ReportTextBox.MouseWheel += ReportTextBox_MouseWheel;
			DataTextBox.MouseWheel += ReportTextBox_MouseWheel;

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

		private async Task<IReport> CreateReport(string text, string json)
		{
			return await Task.Run(async () =>
			{
				var markup = new MarkupParser();

				_json = new Json();
				try
				{
					_json.LoadJson(json);
				}
				catch
				{
				}

				var report = await markup.Parse(text, new ESReportScriptParser(), GetTable);

				return report;
			});
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

			var text = ReportTextBox.Text;

			var json = DataTextBox.Text;

			IEnumerable<Metafile> metafiles = new List<Metafile>();

			IReport report = null;

			await Task.Run(async () =>
			{
				report = await CreateReport(text, json);
			});

			var composer = new Composer();

			var measurer = new TextMeasurer();

			var pages = composer.Compose(report, measurer);

			var mfr = new MetafileRenderer();

			metafiles = mfr.Render(pages);

			foreach (var mf in _preview.Pages)
			{
				mf.Dispose();
			}

			_preview.Pages = metafiles;

			_preview.Refresh();

			_timerLocked = false;
		}

		private void ReportTextBox_TextChanged(object sender, EventArgs e)
		{
			_changed = true;
		}

		private void PrintButton_Click(object sender, EventArgs e)
		{
			var document = new PrintDocument();
			document.BeginPrint += document_BeginPrint;
			document.PrintPage += document_PrintPage;
			printDialog1.Document = document;
			printDialog1.AllowCurrentPage = true;
			printDialog1.AllowSomePages = true;
			if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				document.Print();
			}
			_preview.Focus();
		}

		void document_PrintPage(object sender, PrintPageEventArgs e)
		{
			// no linq
			var n = 0;
			foreach (var p in _preview.Pages)
			{
				if (n++ == _printingPage)
				{
					e.Graphics.DrawImage(p, e.PageBounds);
				}
			}

			_printingPage++;

			e.HasMorePages = _printingPage < n;
		}

		void document_BeginPrint(object sender, PrintEventArgs e)
		{
			_printingPage = 0;
		}
	}
}
