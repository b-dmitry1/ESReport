using System;
using System.Drawing;

namespace ESReport
{
	public interface ICellStyle
	{
		ITextStyle TextStyle { get; set; }
		int Alignment { get; set; }
		string Frame { get; set; }
		Color Color { get; set; }

		ICellStyle Clone();
	}

	public class CellStyle : ICellStyle
	{
		private ITextStyle _textStyle;
		public ITextStyle TextStyle
		{
			get { if (_textStyle == null) _textStyle = new TextStyle(); return _textStyle; }
			set { _textStyle = value; }
		}
	
		private int _alignment;
		public int Alignment
		{
			get { return _alignment; }
			set { _alignment = value; }
		}

		private string _frame;
		public string Frame
		{
			get { return _frame; }
			set { _frame = value; }
		}

		private Color _color = Color.Transparent;
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public ICellStyle Clone()
		{
			return new CellStyle
			{
				_alignment = this._alignment,
				_textStyle = this._textStyle.Clone(),
				_frame = this._frame,
				_color = this._color
			};
		}
	}
}
