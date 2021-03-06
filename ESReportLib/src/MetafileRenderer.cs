using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ESReport
{
	public class MetafileRenderer
	{
		private Metafile RenderPage(IPage page)
		{
			var bmp = new Bitmap(10, 10);

			using (var rg = Graphics.FromImage(bmp))
			{
				var res = new Metafile(rg.GetHdc(), new RectangleF(0, 0, (float)page.Paper.Width * 4.0f, (float)page.Paper.Height * 4.0f), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);

				using (var g = Graphics.FromImage(res))
				{
					foreach (var e in page.Elements)
					{
						var frame = new char[4] { '0', '0', '0', '0' };

						var f = e.Style.Frame;

						if (f == null)
						{
							f = string.Empty;
						}

						if (f.Length == 1)
						{
							frame[0] = f[0];
							frame[1] = f[0];
							frame[2] = f[0];
							frame[3] = f[0];
						}

						if (f.Length == 4)
						{
							frame[0] = f[0];
							frame[1] = f[1];
							frame[2] = f[2];
							frame[3] = f[3];
						}

						var x = e.Position.Left * 4.0f;
						var y = e.Position.Top * 4.0f;
						var w = e.Position.Width * 4.0f;
						var h = e.Position.Height * 4.0f;

						if (e.Style.Color != Color.Transparent)
						{
							g.FillRectangle(new SolidBrush(e.Style.Color), x, y, w, h);
						}

						for (int side = 0; side < 4; side++)
						{
							float x1 = 0, y1 = 0, x2 = 0, y2 = 0;

							switch (side)
							{
								case 0:
									x1 = e.Position.Left * 4.0f;
									y1 = e.Position.Top * 4.0f;
									x2 = x1;
									y2 = y1 + e.Position.Height * 4.0f;
									break;
								case 1:
									x1 = e.Position.Left * 4.0f;
									y1 = e.Position.Top * 4.0f;
									x2 = x1 + e.Position.Width * 4.0f;
									y2 = y1;
									break;
								case 2:
									x1 = e.Position.Left * 4.0f + e.Position.Width * 4.0f;
									y1 = e.Position.Top * 4.0f;
									x2 = x1;
									y2 = y1 + e.Position.Height * 4.0f;
									break;
								case 3:
									x1 = e.Position.Left * 4.0f;
									y1 = e.Position.Top * 4.0f + e.Position.Height * 4.0f;
									x2 = x1 + e.Position.Width * 4.0f;
									y2 = y1;
									break;
							}

							switch (frame[side])
							{ 
								case '1':
								case '2':
								case '3':
								case '4':
								case '5':
								case '6':
								case '7':
								case '8':
								case '9':
									g.DrawLine(new Pen(Brushes.Black, (int)(frame[side] - '0')), x1, y1, x2, y2);
									break;
							}
						}

						var sf = new StringFormat();

						switch (e.Style.Alignment)
						{
							case 0:
							case 1:
							case 4:
							case 7:
								sf.Alignment = StringAlignment.Near;
								break;
							case 2:
							case 5:
							case 8:
								sf.Alignment = StringAlignment.Center;
								break;
							case 3:
							case 6:
							case 9:
								sf.Alignment = StringAlignment.Far;
								break;
						}

						switch (e.Style.Alignment)
						{
							case 0:
							case 1:
							case 2:
							case 3:
								sf.LineAlignment = StringAlignment.Near;
								break;
							case 4:
							case 5:
							case 6:
								sf.LineAlignment = StringAlignment.Center;
								break;
							case 7:
							case 8:
							case 9:
								sf.LineAlignment = StringAlignment.Far;
								break;
						}

						x += (float)(e.Style.Margins.Left * 4.0);
						y += (float)(e.Style.Margins.Top * 4.0);
						w -= (float)((e.Style.Margins.Left + e.Style.Margins.Right) * 4.0);
						h -= (float)((e.Style.Margins.Top + e.Style.Margins.Bottom) * 4.0);

						g.DrawString(e.Text, e.Style.TextStyle.GetFont(), Brushes.Black, new RectangleF(x, y, w, h), sf);
					}
				}
				return res;
			}
		}

		public IEnumerable<Metafile> Render(IEnumerable<IPage> pages)
		{
			var res = new List<Metafile>();

			foreach (var p in pages)
			{
				var metafile = RenderPage(p);
				res.Add(metafile);
			}

			return res;
		}
	}
}
