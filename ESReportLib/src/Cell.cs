using System;
using System.Collections.Generic;

namespace ESReport
{
	public interface ICell : IReportBase
	{
		double Width { get; set; }
		string Text { get; set; }
		bool ExpandDown { get; set; }
		IEnumerable<IRow> Rows { get; }

		double CalcHeight(ITextMeasurer measurer);
	}

	public class Cell : ICell
	{
		private List<IRow> _rows = new List<IRow>();
		private ICellStyle _style = new CellStyle();

		private double _width;
		public double Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public IEnumerable<IRow> Rows
		{
			get { return _rows; }
		}

		private string _text = String.Empty;
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		private bool _expandDown;
		public bool ExpandDown
		{
			get { return _expandDown; }
			set { _expandDown = value; }
		}

		public double CalcHeight(ITextMeasurer measurer)
		{
			double res = measurer.Measure(Text, Style.TextStyle, _width - _style.Margins.Left - _style.Margins.Right).Height;

			double y = 0.0;
			foreach (var row in _rows)
			{
				y += row.CalcHeight(measurer);
			}

			if (res < y)
			{
				res = y;
			}

			return _style.Margins.Top + res + _style.Margins.Bottom;
		}

		public ICellStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}

		public IRow AddRow(double height)
		{
			var row = new Row();
			row.Height = height;
			_rows.Add(row);
			return row;
		}

		public Cell(double width)
		{
			_width = width;
		}
	}
}
