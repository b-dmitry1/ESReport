using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ESReport
{
	public partial class ReportPreview : UserControl
	{
		private IEnumerable<Metafile> _pages = new List<Metafile>();
		public IEnumerable<Metafile> Pages { get { return _pages; } set { _pages = value; } }

		private double _zoom = 1.0;
		public double Zoom { get { return _zoom; } set { if (value > 0.01) _zoom = value; } }

		private int _hOffset = 0;
		public int HOffset { get { return _hOffset; } set { _hOffset = value; } }
		private int _vOffset = 0;
		public int VOffset { get { return _vOffset; } set { _vOffset = value; } }

		public ReportPreview()
		{
			InitializeComponent();
		}

		private int CalcTotalHeight()
		{
			var res = 8;

			foreach (var page in _pages)
			{
				res += (int)(page.Height * _zoom) + 8;
			}

			return res;
		}

		private void ReportPreview_Paint(object sender, PaintEventArgs e)
		{
			using (var bmp = new Bitmap(ClientSize.Width, ClientSize.Height))
			{
				using (var g = Graphics.FromImage(bmp))
				{
					g.FillRectangle(Brushes.DarkGray, new Rectangle(0, 0, bmp.Width, bmp.Height));

					var x = 8 - _hOffset;
					var y = 8 - _vOffset;

					foreach (var page in _pages)
					{
						g.FillRectangle(Brushes.White, new Rectangle(x, y, (int)(page.Width * _zoom), (int)(page.Height * _zoom)));

						g.DrawRectangle(Pens.Black, new Rectangle(x, y, (int)(page.Width * _zoom), (int)(page.Height * _zoom)));

						g.DrawImage(page, new Rectangle(x, y, (int)(page.Width * _zoom), (int)(page.Height * _zoom)));

						y += (int)(page.Height * _zoom) + 8;
					}
				}

				e.Graphics.DrawImageUnscaled(bmp, 0, 0);
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			ProcessMouseWheel(e.Delta);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{ 
				case Keys.Up:
					_vOffset = _vOffset > 50 ? _vOffset - 50 : 0;
					Refresh();
					return true;
				case Keys.Down:
					var h = CalcTotalHeight();
					_vOffset = _vOffset < h - 50 ? _vOffset + 50 : h - 50;
					Refresh();
					return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		public void ProcessMouseWheel(int delta)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				if (delta > 0)
				{
					_zoom += 0.1;
				}
				if (delta < 0)
				{
					if (_zoom > 0.2)
					{
						_zoom -= 0.1;
					}
				}
				Refresh();
				return;
			}

			if (delta > 0)
			{
				_vOffset = _vOffset > delta ? _vOffset - delta : 0;
			}
			else
			{
				var h = CalcTotalHeight();
				_vOffset = _vOffset < h + delta ? _vOffset - delta : h + delta;
			}
			Refresh();
		}
	}
}
