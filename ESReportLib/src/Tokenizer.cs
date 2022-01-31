using System;
using System.Text;

namespace ESReport
{
	public class Tokenizer
	{
		private int _tabSize = 8;
		public int TabSize { get { return _tabSize; } set { _tabSize = value; } }

		private string _text = string.Empty;
		private int _pos;
		private int _line;
		private int _symbol;

		private string _current = string.Empty;
		public string Current { get { return _current; } }

		public bool Eof { get { return _pos >= _text.Length; } }

		public Tokenizer(string text)
		{
			_text = text;
			_pos = 0;
			_line = 0;
			_symbol = 0;
		}

		private void skipWhitespace()
		{
			while (_pos < _text.Length)
			{
				switch (_text[_pos])
				{
					case ' ':
						_symbol++;
						break;
					case '\t':
						_symbol += 8;
						break;
					case '\r':
						break;
					case '\n':
						_symbol = 0;
						_line++;
						break;
					case '/':
						if (_pos + 1 < _text.Length)
						{
							if (_text[_pos + 1] == '/')
							{
								for (; _pos < _text.Length; _pos++)
								{
									if (_text[_pos] == '\n')
									{
										break;
									}
								}
								continue;
							}
						}
						return;
					default:
						return;
				}
				_pos++;
			}
		}

		public bool IsAnIdentifier(string token)
		{
			if (token.Length == 0)
			{
				return false;
			}

			var ch = token[0];
			
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_');
		}

		public bool IsANumber(string token)
		{
			if (token.Length == 0)
			{
				return false;
			}

			var ch = token[0];

			return (ch >= '0' && ch <= '9');
		}

		public string ReadToken()
		{
			skipWhitespace();

			_current = readToken();

			return _current;
		}

		private string readToken()
		{ 
			if (_pos >= _text.Length)
			{
				return "";
			}

			var ch = _text[_pos++];

			_symbol++;

			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_') || (ch == '.') || (ch >= '0' && ch <= '9'))
			{
				var res = new StringBuilder();

				res.Append(ch);

				while (_pos < _text.Length)
				{
					ch = _text[_pos];
					if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_') || (ch == '.') || (ch >= '0' && ch <= '9'))
					{
						res.Append(ch);
						_pos++;
						_symbol++;
					}
					else
					{
						break;
					}
				}

				return res.ToString();
			}

			if (ch == '\"')
			{
				var res = new StringBuilder();

				while (_pos < _text.Length)
				{
					ch = _text[_pos++];
					_symbol++;
					if (ch == '\"')
					{
						break;
					}
					res.Append(ch);
				}

				return res.ToString();
			}

			return ch.ToString();
		}
	}
}
