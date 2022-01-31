using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ESReport
{
	public interface IMarkupParser
	{
		Task<IReport> Parse(string markup, IReportScriptParser parser, Func<string, IEnumerable<IDictionary<string, string>>> dataProvider);
	}

	public class MarkupParser : IMarkupParser
	{
		private Func<string, IEnumerable<IDictionary<string, string>>> _dataProvider;
		private Dictionary<string, string> _vars = new Dictionary<string, string>();

		public MarkupParser()
		{
		}

		private ICellStyle readStyle(IReportScriptElement node, ICellStyle template)
		{
			var res = template.Clone();

			if (node.Attributes.ContainsKey("frame"))
			{
				res.Frame = node["frame"];
			}

			if (node.Attributes.ContainsKey("fontsize"))
			{
				res.TextStyle.FontSize = node.GetDouble("fontsize", 10.0);
			}

			if (node.Attributes.ContainsKey("align"))
			{
				res.Alignment = node.GetInt("align", 1);
			}

			if (node.Attributes.ContainsKey("color"))
			{
				res.Color = Color.FromName(node["color"]);
			}

			return res;
		}

		private double toDouble(string value)
		{
			double res = 0.0;
			double div = 0.0;
			int i;
			char ch;

			for (i = 0; i < value.Length; i++)
			{
				ch = value[i];

				if (ch >= '0' && ch <= '9')
				{
					res *= 10.0;
					res += ch - '0';
				}
				else if ((ch == '.') || (ch == ','))
				{
					div = 1.0;
					continue;
				}
				else
				{
					break;
				}

				if (div > 0.01)
				{
					div *= 10.0;
				}
			}

			if (div > 0.01)
			{
				res /= div;
			}

			return res;
		}

		private string getValue(string varname)
		{
			string res = "";
			if (_vars.ContainsKey(varname))
			{
				res = _vars[varname];
			}
			return res;
		}

		private void ParseCell(IReportScriptElement node, IReportBase elem, IDictionary<string, string> tableRow)
		{
			IRow row;

			switch (node.Name)
			{
				case "row":
					row = elem.AddRow(node.GetDouble("h", 5.0));

					row.Style = readStyle(node, elem.Style);

					foreach (var item in node.Items)
					{
						ParseRow(item, row, tableRow);
					}

					break;
				case "style":
					elem.Style = readStyle(node, elem.Style);
					break;
				case "foreach":
					var table = node["data"];

					var data = _dataProvider(table);

					foreach (var thisTableRow in data)
					{
						foreach (var f in thisTableRow.Keys)
						{
							_vars[f] = thisTableRow[f];
						}

						foreach (var item in node.Items)
						{
							ParseCell(item, elem, thisTableRow);
						}
					}

					break;
				case "assign":
					DoAssign(node);
					break;
			}
		}

		private void ParseRow(IReportScriptElement node, IRow row, IDictionary<string, string> tableRow)
		{
			switch (node.Name)
			{
				case "cell":
					var cell = row.AddCell(node.GetDouble("w", 20.0));

					var s = node["text"];

					s = InsertVariables(s);

					cell.Text = s;

					cell.Style = readStyle(node, row.Style);

					foreach (var subitem in node.Items)
					{
						ParseCell(subitem, cell, tableRow);
					}

					break;
				case "assign":
					DoAssign(node);
					break;
				case "foreach":
					var table = node["data"];

					var data = _dataProvider(table);

					foreach (var thisTableRow in data)
					{
						foreach (var f in thisTableRow.Keys)
						{
							_vars[f] = thisTableRow[f];
						}

						foreach (var item in node.Items)
						{
							ParseRow(item, row, thisTableRow);
						}
					}

					break;
			}
		}

		private string InsertVariables(string s)
		{
			foreach (var v in _vars.Keys)
			{
				var p = s.IndexOf("{" + v + ":");
				if (p >= 0)
				{
					var colon = s.IndexOf(":", p);
					if (colon > 0)
					{
						var end = s.IndexOf("}", colon);
						var fmt = s.Substring(colon + 1, end - colon - 1);
						s = s.Replace("{" + v + ":" + fmt + "}", string.Format("{0:" + fmt + "}", toDouble(_vars[v])));
					}
				}
				if (s.IndexOf("{" + v + "}") >= 0)
				{
					s = s.Replace("{" + v + "}", _vars[v].ToString());
				}
			}
			return s;
		}

		private void DoAssign(IReportScriptElement node)
		{
			double v1, v2;

			var varname = node["var"];

			var stack = new Stack<string>();

			foreach (var instr in node.Items)
			{
				switch (instr.Name)
				{
					case "push":
						stack.Push(getValue(instr["var"]));
						break;
					case "pushn":
						v1 = toDouble(instr["n"]);
						stack.Push(v1.ToString());
						break;
					case "add":
						v1 = toDouble(stack.Pop());
						v2 = toDouble(stack.Pop());
						stack.Push((v2 + v1).ToString());
						break;
					case "sub":
						v1 = toDouble(stack.Pop());
						v2 = toDouble(stack.Pop());
						stack.Push((v2 - v1).ToString());
						break;
					case "mul":
						v1 = toDouble(stack.Pop());
						v2 = toDouble(stack.Pop());
						stack.Push((v2 * v1).ToString());
						break;
					case "div":
						v1 = toDouble(stack.Pop());
						v2 = toDouble(stack.Pop());
						if (v1 == 0.0)
						{
							v1 = 1.0;
						}
						stack.Push((v2 / v1).ToString());
						break;
				}
			}

			_vars[varname] = stack.Pop();
		}

		public Task<IReport> Parse(string markup, IReportScriptParser parser, Func<string, IEnumerable<IDictionary<string, string>>> dataProvider)
		{
			_dataProvider = dataProvider;

			_vars = new Dictionary<string, string>();

			return Task<IReport>.Run(() =>
			{
				var tree = parser.Parse(markup);

				IReport report = new Report();

				foreach (var element in tree.Items)
				{
					ParseCell(element, report, new Dictionary<string, string>());
				}

				return report;
			});
		}
	}
}
