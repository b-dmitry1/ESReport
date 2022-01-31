using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ESReport
{
	public interface IReportBase
	{
		IRow AddRow(double height);
		ICellStyle Style { get; set; }
	}

	public interface IReport : IReportBase
	{
		IPaper Paper { get; }
		IEnumerable<IRow> Rows { get; }
	}

	public class Report : IReport
	{
		private List<IRow> _rows = new List<IRow>();
		public IEnumerable<IRow> Rows
		{
			get { return _rows; }
		}

		public IRow AddRow(double height)
		{
			var row = new Row();
			row.Height = height;
			_rows.Add(row);
			return row;
		}

		private ICellStyle _style;
		public ICellStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}

		public Report()
		{
			_style = new CellStyle();
			_style.Alignment = 4;
			_style.Frame = "";
			_style.TextStyle.FontName = "Arial";
			_style.TextStyle.FontSize = 10.0f;
			_style.TextStyle.FontColor = Color.Black;
		}

		private IPaper _paper = new Paper();
		public IPaper Paper
		{
			get { return _paper; }
		}
	}
}
