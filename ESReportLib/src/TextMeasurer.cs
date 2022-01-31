using System;
using System.Collections.Generic;
using System.Drawing;

namespace ESReport
{
	public interface ITextMeasurer
	{
		SizeF Measure(string text, ITextStyle style);
		SizeF Measure(string text, ITextStyle style, double fitWidth);
		void EndMeasurements();
	}

	public class TextMeasurer : ITextMeasurer
	{
		private static Bitmap _bitmap = new Bitmap(10, 10);
		private static Graphics _graphics = null;

		public SizeF Measure(string text, ITextStyle style)
		{
			return Measure(text, style, 0);
		}

		public SizeF Measure(string text, ITextStyle style, double fitWidth)
		{
			var res = new SizeF((float)fitWidth, 0.0f);
			SizeF sz;

			if (_graphics == null)
			{
				_graphics = Graphics.FromImage(_bitmap);
			}

			sz = _graphics.MeasureString(text.Length > 0 ? text : "A", new Font("Arial", 10.0f));

			sz.Width = (float)Math.Round(sz.Width, 2);
			sz.Height = (float)Math.Round(sz.Height, 2);

			sz.Width /= 4.0f;
			sz.Height /= 4.0f;

			return sz;
		}

		public void EndMeasurements()
		{
			if (_graphics != null)
			{
				_graphics.Dispose();
				_graphics = null;
			}
		}
	}
}
