using System;
using System.Drawing;
using System.Collections.Generic;

namespace ESReport
{
	public interface ITextStyle
	{
		string FontName { get; set; }
		double FontSize { get; set; }
		bool FontBold { get; set; }
		bool FontItalic { get; set; }
		bool FontUnderline { get; set; }
		Color FontColor { get; set; }

		Font GetFont();

		ITextStyle Clone();
	}

	public struct Margins
	{
		public double Left;
		public double Right;
		public double Top;
		public double Bottom;

		public Margins(double value = 0.0f)
		{
			Left = value;
			Right = value;
			Top = value;
			Bottom = value;
		}
	}

	public struct Padding
	{
		public double Left;
		public double Right;
		public double Top;
		public double Bottom;

		public Padding(double value = 0.0f)
		{
			Left = value;
			Right = value;
			Top = value;
			Bottom = value;
		}
	}

	public class TextStyle : ITextStyle
	{
		private Font _font;

		private string _fontName = "Arial";
		public string FontName
		{
			get { return _fontName; }
			set { _fontName = value; }
		}

		private double _fontSize = 10.0;
		public double FontSize
		{
			get { return _fontSize; }
			set { _fontSize = value; }
		}

		private bool _fontBold = false;
		public bool FontBold
		{
			get { return _fontBold; }
			set { _fontBold = value; }
		}

		private bool _fontItalic = false;
		public bool FontItalic
		{
			get { return _fontItalic; }
			set { _fontItalic = value; }
		}

		private bool _fontUnderline = false;
		public bool FontUnderline
		{
			get { return _fontUnderline; }
			set { _fontUnderline = value; }
		}

		private Color _fontColor = Color.Black;
		public Color FontColor
		{
			get { return _fontColor; }
			set { _fontColor = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}_{1}_{2}",
				_fontName,
				(int)(_fontSize * 100.0f + 0.0001f),
				(_fontBold ? "b" : "") + (_fontItalic ? "i" : "") + (_fontUnderline ? "u" : ""));
		}

		public Font GetFont()
		{
			if (_font != null)
			{
				if (_font.Name != _fontName ||
					_fontSize != _font.Size ||
					_fontBold != _font.Bold ||
					_fontItalic != _font.Italic ||
					_fontUnderline != _font.Underline)
				{
					_font = null;
				}
			}

			if (_font == null)
			{
				FontStyle fontstyle = 0;
				if (_fontBold)
				{
					fontstyle |= FontStyle.Bold;
				}
				if (_fontItalic)
				{
					fontstyle |= FontStyle.Italic;
				}
				if (_fontUnderline)
				{
					fontstyle |= FontStyle.Underline;
				}
				_font = new Font(_fontName, (float)_fontSize, fontstyle);
			}

			return _font;
		}

		public ITextStyle Clone()
		{
			return new TextStyle
			{
				_fontBold = this._fontBold,
				_fontColor = this._fontColor,
				_fontItalic = this._fontItalic,
				_fontName = this._fontName,
				_fontSize = this._fontSize,
				_fontUnderline = this._fontUnderline
			};
		}
	}
}
