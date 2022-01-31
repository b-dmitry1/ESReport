using System;
using System.Collections.Generic;

namespace ESReport
{
	public interface IReportScript
	{
		
	}

	public interface IReportScriptElement
	{
		string Name { get; set; }
		IDictionary<string, string> Attributes { get; }
		IEnumerable<IReportScriptElement> Items { get; }

		void SetAttribute(string name, string value);
		IReportScriptElement AddItem(string name);

		int GetInt(string name, int def = 0);
		double GetDouble(string name, double def = 0.0);

		string this[string index] { get; set; }
	}

	public class ReportScriptElement : IReportScriptElement
	{
		public string Name { get; set; }

		private Dictionary<string, string> _attributes = new Dictionary<string, string>();
		public IDictionary<string, string> Attributes { get { return _attributes; } }

		private List<IReportScriptElement> _items = new List<IReportScriptElement>();
		public IEnumerable<IReportScriptElement> Items { get { return _items; } }

		public void SetAttribute(string name, string value)
		{
			_attributes[name] = value;
		}

		public IReportScriptElement AddItem(string name)
		{
			var res = new ReportScriptElement();

			res.Name = name;

			_items.Add(res);

			return res;
		}

		public int GetInt(string name, int def = 0)
		{
			if (!_attributes.ContainsKey(name))
			{
				return def;
			}
			int value = 0;
			if (!int.TryParse(_attributes[name], out value))
				value = def;
			return value;
		}

		public double GetDouble(string name, double def = 0.0)
		{
			if (!_attributes.ContainsKey(name))
			{
				return def;
			}
			double value = def;
			if (!double.TryParse(_attributes[name], out value))
				value = def;
			return value;
		}

		public string this[string index]
		{
			get
			{
				if (!_attributes.ContainsKey(index))
				{
					return "";
				}
				return _attributes[index];
			}
			set
			{
				_attributes[index] = value;
			}
		}
	}
}
