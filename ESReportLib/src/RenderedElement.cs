using System;
using System.Drawing;

namespace ESReport
{
	public interface IRenderedElement
	{
		RectangleF Position { get; set; }
		ICellStyle Style { get; set; }
		string Text { get; set; }
	}

	public class RenderedElement : IRenderedElement
	{
		public string Text { get; set; }

		private RectangleF _position;
		public RectangleF Position
		{
			get { return _position; }
			set { _position = value; }
		}

		ICellStyle _style;
		public ICellStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}, {3}", _position.Left, _position.Top, _position.Width, _position.Height);
		}
	}
}
