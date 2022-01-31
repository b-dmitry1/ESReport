using System;
using System.Collections.Generic;

namespace ESReport
{
	public interface IRow
	{
		double Height { get; set; }
		IEnumerable<ICell> Cells { get; }
		ICellStyle Style { get; set; }
		string Data { get; set; }

		ICell AddCell(double width);
		double CalcHeight(ITextMeasurer measurer);
	}

	public class Row : IRow
	{
		private List<ICell> _cells = new List<ICell>();

		public string Data { get; set; }

		private ICellStyle _style;
		public ICellStyle Style
		{
			get { return _style; }
			set { _style = value;  }
		}

		private double _height;
		public double Height
		{
			get { return _height; }
			set { _height = value; }
		}

		private int _alignment;
		public int Alignment
		{
			get { return _alignment; }
			set { _alignment = value; }
		}

		public IEnumerable<ICell> Cells
		{
			get { return _cells; }
		}

		public ICell AddCell(double width)
		{
			var res = new Cell(width);
			_cells.Add(res);
			return res;
		}

		public double CalcHeight(ITextMeasurer measurer)
		{
			var res = Height;
			foreach (var cell in _cells)
			{
				var h = cell.CalcHeight(measurer);
				if (res < h)
				{
					res = h;
				}
			}
			return res;
		}
	}
}
