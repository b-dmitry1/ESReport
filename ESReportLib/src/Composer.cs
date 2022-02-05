using System;
using System.Collections.Generic;
using System.Drawing;

namespace ESReport
{
	public interface IComposer
	{
		List<IPage> Compose(IReport report, ITextMeasurer measurer);
	}

	public class Composer : IComposer
	{
		public List<IPage> Compose(IReport report, ITextMeasurer measurer)
		{
			var pages = new List<IPage>();

			try
			{
				var style = report.Style;

				var page = new Page(report.Paper);
				pages.Add(page);

				var x = page.Paper.Margins.Left;
				var y = page.Paper.Margins.Top;

				foreach (var row in report.Rows)
				{
					var height = row.CalcHeight(measurer);

					if (y + height > page.Paper.Height - page.Paper.Margins.Bottom || row.PageBreak)
					{
						page = new Page(report.Paper);
						pages.Add(page);

						x = page.Paper.Margins.Left;
						y = page.Paper.Margins.Top;
					}

					Compose(page, measurer, row, style, x, y, height);
					y += height;
				}
			}
			finally
			{
				measurer.EndMeasurements();
			}
			return pages;
		}

		private void Compose(IPage page, ITextMeasurer measurer, IRow row, ICellStyle style, double Left, double Top, double Height)
		{
			double x = Left;
			foreach (var cell in row.Cells)
			{
				var element = new RenderedElement();
				var width = cell.Width;
				element.Position = new RectangleF((float)x, (float)Top, (float)width, (float)Height);
				element.Style = cell.Style;
				element.Text = cell.Text;
				page.AddElement(element);
				Compose(page, measurer, cell, row.Style, x, Top, Height);
				x += width;
			}
		}

		private void Compose(IPage page, ITextMeasurer measurer, ICell cell, ICellStyle style, double Left, double Top, double Height)
		{
			double y = Top;
			foreach (var row in cell.Rows)
			{
				var height = row.CalcHeight(measurer);
				Compose(page, measurer, row, style, Left, y, height);
				y += height;
			}
		}
	}
}
