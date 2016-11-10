using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>константы CHAR</summary>
	public static class C1
	{
		///<summary>Правая пара к скобке (остальные символы возвращаются сами)</summary>
		public static char Right(char left)
		{
			if (left == LBrace) { return RBrace; }//if
			else if (left == LParenthesis) { return RParenthesis; }//if
			else if (left == LSquare) { return RSquare; }//if
			else if (left == Less) { return Greater; }//if
			else {return left;}//else
		}//function

		///<summary>@</summary>
		public const char Dog = '@';
		///<summary>*</summary>
		public const char Star = '*';
		public const char Tab = '\t';
		public const char Space = ' ';
		public const char Point = '.';
		public const char Comma = ',';
		public const char Colon = ':';
		public const char Semicolon = ';';
		public const char Slash = '/';
		///<summary>кавычка одиночная</summary>
		public const char Quote = '\'';
		///<summary>кавычка двойная</summary>
		public const char QuoteDouble = '\"';
		
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

		///<summary>меньше</summary>
		public const char Less = '<';
		///<summary>больше</summary>
		public const char Greater = '>';
	}//class

	///<summary>константы STRING</summary>
	public static class S1
	{
		///<summary>Правая пара к скобке (остальные символы возвращаются сами)</summary>
		public static string Right(string left)
		{
			if (left == LBrace) { return RBrace; }//if
			else if (left == LParenthesis) { return RParenthesis; }//if
			else if (left == LSquare) { return RSquare; }//if
			else if (left == Less) { return Greater; }//if
			else { return left; }//else
		}//function

		///<summary>@</summary>
		public const string Dog = "@";
		///<summary>*</summary>
		public const char Star = '*';
		public const string Tab = "\t";
		public const string Space = " ";
		public const string Point = ".";
		public const string Comma = ",";
		public const string Colon = ":";
		public const string Semicolon = ";";
		public const string Slash = "/";
		///<summary>кавычка одиночная</summary>
		public const string Quote = "\'";
		///<summary>кавычка двойная</summary>
		public const string QuoteDouble = "\"";
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
		///<summary>меньше</summary>
		public const string Less = "<";
		///<summary>больше</summary>
		public const string Greater = ">";

	}//class
}//namespace
