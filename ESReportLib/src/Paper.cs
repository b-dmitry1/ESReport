using System;
using System.Drawing;

namespace ESReport
{
	public interface IPaper
	{
		double Width { get; set; }
		double Height { get; set; }
		double LeftMargin { get; set; }
		double TopMargin { get; set; }
		double RightMargin { get; set; }
		double BottomMargin { get; set; }
	}

	public class Paper : IPaper
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public double LeftMargin { get; set; }
		public double TopMargin { get; set; }
		public double RightMargin { get; set; }
		public double BottomMargin { get; set; }

		public Paper()
		{
			Width = 210.0f;
			Height = 297.0f;
			LeftMargin = 10.0f;
			TopMargin = 10.0f;
			RightMargin = 10.0f;
			BottomMargin = 10.0f;
		}
	}
}
