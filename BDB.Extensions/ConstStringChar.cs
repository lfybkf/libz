


//Generated 10.11.2016 13:58:45

namespace BDB
{
	///<summary>константы CHAR</summary>
	public static class C
	{
				///<summary>амперсанд</summary>
public const char Ampersand = '&';
				///<summary>\\</summary>
public const char BackSlash = '\\';
				///<summary>@ (собака)</summary>
public const char Dog = '@';
				///<summary>= (равно)</summary>
public const char Eq = '=';
				///<summary>! (восклицательный знак)</summary>
public const char Exclamation = '!';
				///<summary>, (запятая)</summary>
public const char Comma = ',';
				///<summary>: (двоеточие))</summary>
public const char Colon = ':';
				///<summary>- (минус)</summary>
public const char Minus = '-';
				///<summary>+ (плюс)</summary>
public const char Plus = '+';
				///<summary>^</summary>
public const char Power = '^';
				///<summary>. (точка)</summary>
public const char Point = '.';
				///<summary>% (процент)</summary>
public const char Percent = '%';
				///<summary>? (вопрос)</summary>
public const char Question = '?';
				///<summary>\' (кавычка одиночная)</summary>
public const char Quote = '\'';
				///<summary>\" (кавычка двойная)</summary>
public const char QuoteDouble = '\"';
				///<summary>#</summary>
public const char Sharp = '#';
				///<summary>/</summary>
public const char Slash = '/';
				///<summary>; (точка с запятой)</summary>
public const char Semicolon = ';';
				///<summary>  (пробел)</summary>
public const char Space = ' ';
				///<summary>*</summary>
public const char Star = '*';
				///<summary>символ табуляции</summary>
public const char Tab = '	';
				///<summary>_ (подчеркивание)</summary>
public const char Under = '_';
				///<summary>(</summary>
public const char LParenthesis = '(';
				///<summary>)</summary>
public const char RParenthesis = ')';
				///<summary>[</summary>
public const char LSquare = '[';
				///<summary>]</summary>
public const char RSquare = ']';
				///<summary>{</summary>
public const char LBrace = '{';
				///<summary>}</summary>
public const char RBrace = '}';
				///<summary>знак меньше</summary>
public const char Less = '<';
				///<summary>знак больше</summary>
public const char Greater = '>';
		
		///<summary>Правая пара к скобке (остальные символы возвращаются сами)</summary>
		public static char Right(char left)
		{
			if (left == LBrace) { return RBrace; }//if
			else if (left == LParenthesis) { return RParenthesis; }//if
			else if (left == LSquare) { return RSquare; }//if
			else if (left == Less) { return Greater; }//if
			else { return left; }//else
		}//function
	}//class

	///<summary>константы STRING</summary>
	public static class S
	{
				///<summary>амперсанд</summary>
public const string Ampersand = "&";
				///<summary>\\</summary>
public const string BackSlash = "\\";
				///<summary>@ (собака)</summary>
public const string Dog = "@";
				///<summary>= (равно)</summary>
public const string Eq = "=";
				///<summary>! (восклицательный знак)</summary>
public const string Exclamation = "!";
				///<summary>, (запятая)</summary>
public const string Comma = ",";
				///<summary>: (двоеточие))</summary>
public const string Colon = ":";
				///<summary>- (минус)</summary>
public const string Minus = "-";
				///<summary>+ (плюс)</summary>
public const string Plus = "+";
				///<summary>^</summary>
public const string Power = "^";
				///<summary>. (точка)</summary>
public const string Point = ".";
				///<summary>% (процент)</summary>
public const string Percent = "%";
				///<summary>? (вопрос)</summary>
public const string Question = "?";
				///<summary>\' (кавычка одиночная)</summary>
public const string Quote = "\'";
				///<summary>\" (кавычка двойная)</summary>
public const string QuoteDouble = "\"";
				///<summary>#</summary>
public const string Sharp = "#";
				///<summary>/</summary>
public const string Slash = "/";
				///<summary>; (точка с запятой)</summary>
public const string Semicolon = ";";
				///<summary>  (пробел)</summary>
public const string Space = " ";
				///<summary>*</summary>
public const string Star = "*";
				///<summary>символ табуляции</summary>
public const string Tab = "	";
				///<summary>_ (подчеркивание)</summary>
public const string Under = "_";
				///<summary>(</summary>
public const string LParenthesis = "(";
				///<summary>)</summary>
public const string RParenthesis = ")";
				///<summary>[</summary>
public const string LSquare = "[";
				///<summary>]</summary>
public const string RSquare = "]";
				///<summary>{</summary>
public const string LBrace = "{";
				///<summary>}</summary>
public const string RBrace = "}";
				///<summary>знак меньше</summary>
public const string Less = "<";
				///<summary>знак больше</summary>
public const string Greater = ">";
		
		///<summary>Правая пара к скобке (остальные символы возвращаются сами)</summary>
		public static string Right(string left)
		{
			if (left == LBrace) { return RBrace; }//if
			else if (left == LParenthesis) { return RParenthesis; }//if
			else if (left == LSquare) { return RSquare; }//if
			else if (left == Less) { return Greater; }//if
			else { return left; }//else
		}//function
	}//class
}


