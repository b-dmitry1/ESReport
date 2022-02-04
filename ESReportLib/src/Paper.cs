using System;
using System.Drawing;

namespace ESReport
{
	public interface IPaper
	{
		double Width { get; set; }
		double Height { get; set; }
		Margins Margins { get; set; }
	}

	public class Paper : IPaper
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public Margins Margins { get; set; }

		public Paper()
		{
			Width = 210.0f;
			Height = 297.0f;
			Margins = new ESReport.Margins(10.0f);
		}
	}
}
