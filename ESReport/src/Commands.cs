using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ESReport
{
	public interface ICommand
	{
		void Execute();
	}

	public class SaveCommand : ICommand
	{
		private string _fileName;
		private string _content;

		public SaveCommand(string fileName, string content)
		{
			_fileName = fileName;
			_content = content;
		}

		public void Execute()
		{
			try
			{
				File.WriteAllText(_fileName, _content);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Cannot save file \"{0}\": {1}", _fileName, ex.Message));
			}
		}
	}

	public class PrintCommand : ICommand
	{
		IEnumerable<Metafile> _pages;

		public PrintCommand(IEnumerable<Metafile> pages)
		{
			_pages = pages;
		}

		public void Execute()
		{
			try
			{
				new DocumentPrinter()
					.Print(_pages);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Error while trying to print document: {0}", ex.Message));
			}
		}
	}
}
