using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Json
{
	public string Name = String.Empty;
	public List<Json> Items = new List<Json>();
	private string val = String.Empty;
	private bool isArray = false;
	private bool isNumber = false;

	public int IntValue
	{
		get
		{
			isNumber = true;
			int v;
			if (!int.TryParse(val, out v))
				v = 0;
			return v;
		}
		set
		{
			isNumber = true;
			val = value.ToString();
		}
	}

	public double DoubleValue
	{
		get
		{
			isNumber = true;
			double v;
			if (!double.TryParse(val, out v))
				v = 0;
			return v;
		}
		set
		{
			isNumber = true;
			val = value.ToString();
		}
	}

	public string StrValue
	{
		get
		{
			isNumber = false;
			return val;
		}
		set
		{
			isNumber = false;
			val = value;
		}
	}

	public Json AddArray(string name)
	{
		var v = new Json { Name = name, isArray = true };
		Items.Add(v);
		return v;
	}

	public void AddArrayItem(string value)
	{
		Items.Add(new Json { val = value });
	}

	public Json Add(string name)
	{
		var v = new Json { Name = name };
		Items.Add(v);
		return v;
	}

	public string Dump(string tab, int tabs)
	{
		var s = new StringBuilder();
		int i;

		for (i = 0; i < tabs; i++)
			s.Append(tab);

		s.Append(Name);

		if (Items.Count > 0)
		{
			if (Name.Length > 0)
				s.Append(" ");
			if (isArray)
				s.AppendLine("[");
			else
				s.AppendLine("{");
			foreach (var v in Items)
				s.Append(v.Dump(tab, tabs + 1));
			for (i = 0; i < tabs; i++)
				s.Append(tab);
			if (isArray)
				s.AppendLine("]");
			else
				s.AppendLine("}");
		}
		else
		{
			s.AppendLine(" = " + val);
		}

		return s.ToString();
	}

	private bool strIsNumber(string v)
	{
		foreach (var ch in v)
			if (!(((ch >= '0') && (ch <= '9')) || (ch == '.') || (ch == ',')))
				return false;
		return true;
	}

	public string SaveJson(string tab, int tabs, bool noname = false)
	{
		var s = new StringBuilder();
		int i;

		for (i = 0; i < tabs; i++)
			s.Append(tab);

		if (!noname)
		{
			if (strIsNumber(Name))
			{
				s.Append(Name);
			}
			else
			{
				if (Name.Length > 0)
					s.Append("\"");
				s.Append(Name);
				if (Name.Length > 0)
					s.Append("\"");
			}
			if (Name.Length > 0)
				s.Append(":");
		}

		if (Items.Count > 0)
		{
			if ((Name.Length > 0) && (!noname))
				s.Append(" ");
			if (isArray)
				s.AppendLine("[");
			else
				s.AppendLine("{");
			var first = true;
			foreach (var v in Items)
			{
				if (!first)
					s.AppendLine(",");
				first = false;
				s.Append(v.SaveJson(tab, tabs + 1, isArray));
			}
			if (!first)
				s.AppendLine();
			for (i = 0; i < tabs; i++)
				s.Append(tab);
			if (isArray)
				s.Append("]");
			else
				s.Append("}");
		}
		else
		{
			s.Append(" ");
			var num = isNumber;
			if (!num)
				s.Append("\"");
			s.Append(val);
			if (!num)
				s.Append("\"");
		}

		return s.ToString();
	}

	public Json this[string index]
	{
		get
		{
			var lower = index.ToLower();
			var v = Items.Find(f => f.Name.ToLower() == lower);
			if (v == null)
			{
				v = new Json();
				v.Name = index;
				Items.Add(v);
				return v;
			}
			return v;
		}
	}

	public void LoadJson(string text)
	{
		var deserializer = new JsonDeserializer();
		deserializer.ParseInto(text, this);
	}

	private enum TokenType
	{
		Eof, String, OpenCur, OpenBracket, CloseCur, CloseBracket, Comma, Colon
	}

	private class JsonDeserializer
	{
		private string text;
		private string token;
		private int pos;

		public JsonDeserializer()
		{
		}

		private void reset()
		{
			pos = 0;
			token = "";
		}

		private TokenType next()
		{
			var tok = new StringBuilder();
			char ch;

			for (; pos < text.Length; pos++)
			{
				ch = text[pos];
				if ((ch != ' ') && (ch != '\t') && (ch != '\r') && (ch != '\n'))
					break;
			}

			if (pos >= text.Length)
			{
				token = "";
				return TokenType.Eof;
			}

			ch = text[pos++];
			switch (ch)
			{
				case '{':
					token = ch.ToString();
					return TokenType.OpenCur;
				case '}':
					token = ch.ToString();
					return TokenType.CloseCur;
				case '[':
					token = ch.ToString();
					return TokenType.OpenBracket;
				case ']':
					token = ch.ToString();
					return TokenType.CloseBracket;
				case ',':
					token = ch.ToString();
					return TokenType.Comma;
				case ':':
					token = ch.ToString();
					return TokenType.Colon;
				case '\"':
					for (; pos < text.Length; pos++)
					{
						ch = text[pos];
						if (ch == '\"')
						{
							pos++;
							break;
						}
						tok.Append(ch);
					}
					token = tok.ToString();
					return TokenType.String;
			}

			if ((ch >= '0') && (ch <= '9'))
			{
				pos--;
				while (((ch >= '0') && (ch <= '9')) || (ch == '.'))
				{
					tok.Append(ch);
					if (pos >= text.Length)
						break;
					ch = text[++pos];
				}
				token = tok.ToString();
				return TokenType.String;
			}

			token = "";
			throw new Exception("Error in JSON document");
		}

		private void parseArray(Json v)
		{
			var done = false;
			v.isArray = true;
			while (!done)
			{
				var t = next();
				switch (t)
				{
					case TokenType.String:
						v.Items.Add(new Json { Name = v.Items.Count.ToString(), val = token });
						break;
					case TokenType.OpenCur:
						var newvar = new Json();
						newvar.Name = v.Items.Count.ToString();
						v.Items.Add(newvar);
						parseObject(newvar);
						break;
					default:
						throw new Exception("Unexpected symbol found while reading an array \"" + v.Name + "\"");
				}

				switch (next())
				{
					case TokenType.Comma:
						break;
					case TokenType.CloseBracket:
						done = true;
						break;
					default:
						throw new Exception("Unexpected separator found while reading an array \"" + v.Name + "\"");
				}
			}
		}

		private void expecting(TokenType t)
		{
			if (t != next())
				throw new Exception("Unexpected symbol");
		}

		private void parseObject(Json v)
		{
			var done = false;
			while (!done)
			{
				var t = next();
				switch (t)
				{
					case TokenType.String:
						var newvar = new Json();
						newvar.Name = token;
						v.Items.Add(newvar);
						expecting(TokenType.Colon);
						parseValue(newvar);
						break;
					default:
						throw new Exception("Cannot find object name");
				}

				switch (next())
				{
					case TokenType.Comma:
						break;
					case TokenType.CloseCur:
						done = true;
						break;
					default:
						throw new Exception("Unexpected separator found while reading an object \"" + v.Name + "\"");
				}
			}
		}

		private void parseValue(Json v)
		{
			switch (next())
			{
				case TokenType.String:
					v.val = token;
					break;
				case TokenType.OpenBracket:
					parseArray(v);
					break;
				case TokenType.OpenCur:
					parseObject(v);
					break;
				default:
					throw new Exception("Cannot find value: \"" + v.Name + "\"");
			}
		}

		public Json Parse(string text)
		{
			var v = new Json();

			this.text = text;

			parseValue(v);

			return v;
		}

		public void ParseInto(string text, Json v)
		{
			this.text = text;
			parseValue(v);
		}
	}
}
