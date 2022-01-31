using System;
using System.Collections.Generic;

namespace ESReport
{
	public interface IReportScriptParser
	{
		IReportScriptElement Parse(string text);
	}

	public class ESReportScriptParser : IReportScriptParser
	{
		private readonly string[] keywords = new string[] { "row", "cell", "style", "foreach", "var", "add" };

		private void CompileMath(Tokenizer tzr, IReportScriptElement elem)
		{
			var op = "";
			while (!tzr.Eof)
			{
				var token = tzr.ReadToken();

				if (tzr.IsANumber(token))
				{
					elem.AddItem("pushn").SetAttribute("n", token);
				}
				else if (tzr.IsAnIdentifier(token))
				{ 
					elem.AddItem("push").SetAttribute("var", token);
				}
				else
				{
					break;
				}

				op = tzr.ReadToken();

				if (op != "+" && op != "-" && op != "*" && op != "/")
				{
					break;
				}

				token = tzr.ReadToken();

				if (tzr.IsANumber(token))
				{
					elem.AddItem("pushn").SetAttribute("n", token);
				}
				else if (tzr.IsAnIdentifier(token))
				{
					elem.AddItem("push").SetAttribute("var", token);
				}
				else
				{
					break;
				}

				switch (op)
				{
					case "+":
						elem.AddItem("add");
						break;
					case "-":
						elem.AddItem("sub");
						break;
					case "*":
						elem.AddItem("mul");
						break;
					case "/":
						elem.AddItem("div");
						break;
				}
			}
		}

		public IReportScriptElement Parse(string text)
		{
			var root = new ReportScriptElement();

			var tzr = new Tokenizer(text);

			var stack = new Stack<IReportScriptElement>();
			stack.Push(root);

			var noRead = false;

			var token = "";

			while (!tzr.Eof)
			{
				if (noRead == false)
				{
					token = tzr.ReadToken();
				}
				else
				{
					noRead = false;
				}

				if (tzr.IsAnIdentifier(token))
				{
					var elem = stack.Peek().AddItem(token);

					var prev = token;

					while (!tzr.Eof)
					{
						token = tzr.ReadToken();

						if (token == "=")
						{
							// Assignment
							elem.Name = "assign";
							elem.SetAttribute("var", prev);

							CompileMath(tzr, elem);

							token = tzr.Current;

							break;
						}

						if (token == ",")
						{
							token = tzr.ReadToken();
							if (token == "\n")
							{
								token = tzr.ReadToken();
							}
						}

						if (!tzr.IsAnIdentifier(token))
						{
							break;
						}
						
						// no linq
						var found = false;
						foreach (var w in keywords)
						{
							if (token == w)
							{
								found = true;
								break;
							}
						}

						if (found)
						{
							break;
						}
						
						if (tzr.ReadToken() != "=")
						{
							break;
						}

						var value = tzr.ReadToken();

						elem.SetAttribute(token, value);
					}

					if (token == "\n")
					{
						token = tzr.ReadToken();
					}

					if (token == "{")
					{
						stack.Push(elem);
					}
					else
					{
						noRead = true;
					}
				}
				else if (token == "}")
				{
					stack.Pop();
					if (stack.Count == 0)
						break;
				}
				else if (token == ";")
				{

				}
				else
				{
					break;
				}
			}

			return root;
		}
	}
}
