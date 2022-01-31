using System;
using System.Collections.Generic;
using System.Drawing;

namespace ESReport
{
	public interface IPage
	{
		IPaper Paper { get; }
		IEnumerable<IRenderedElement> Elements { get; }

		void AddElement(IRenderedElement element);
	}

	public class Page : IPage
	{
		private IPaper _paper;
		public IPaper Paper { get { return _paper; } }

		public List<IRenderedElement> _elements = new List<IRenderedElement>();
		public IEnumerable<IRenderedElement> Elements { get { return _elements; } }

		public Page(IPaper paper)
		{
			_paper = paper;
		}

		public void AddElement(IRenderedElement element)
		{
			_elements.Add(element);
		}
	}
}
