using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ESReport
{
	public class DocumentPrinter
	{
		private IEnumerable<Metafile> _pages = new List<Metafile>();
		private int _printingPage = 0;

		public void Print(IEnumerable<Metafile> pages)
		{
			_pages = pages;
			_printingPage = 0;

			var document = new PrintDocument();
			document.BeginPrint += document_BeginPrint;
			document.PrintPage += document_PrintPage;

			var printDialog = new PrintDialog();
			printDialog.Document = document;
			printDialog.AllowCurrentPage = true;
			printDialog.AllowSomePages = true;

			if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				document.Print();
			}
		}

		void document_PrintPage(object sender, PrintPageEventArgs e)
		{
			var n = 0;
			foreach (var p in _pages)
			{
				if (n++ == _printingPage)
				{
					e.Graphics.DrawImage(p, e.PageBounds);
				}
			}

			_printingPage++;

			e.HasMorePages = _printingPage < n;
		}

		void document_BeginPrint(object sender, PrintEventArgs e)
		{
			_printingPage = 0;
		}

	}
}
