using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BDB
{
	///<summary>расширение строк</summary> 
	public static class StringExtension
	{
		/// <summary>формат строки. usage: "Value1={0}, Value2={1}".fmt(Value1, Value2) </summary>
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }//func

		///<summary>to string</summary> 
		public static string toString(this IEnumerable<char> chars) => new string(chars.ToArray());

		//если эти символы в плейсхолдере, то это не он и его игнорируем
		private static char[] badCharsForPlaceholder = { C.Space, C.Tab, '\n' };
		/// <summary>from "asd{name}qwer{age}ty" get 3,"{name}" ;14,"{age}"</summary>
		internal static IDictionary<int, string> getPlaceholders(this string s, char cL = '{', char cR = '}')
		{
			if (s.IndexOf(cL) < 0) { return null; }//if

			IDictionary<int, string> result = new Dictionary<int, string>();
			char c;
			int iL = -1;
			string ph;
			// "asd{ph}qwerty"
			for (int i = 0; i < s.Length; i++)
			{
				c = s[i];
				if (c == cL)
				{
					iL = i;//i=3
				}//if
				else if (c == cR && iL >= 0) //i=6
				{
					ph = s.Substring(iL, i - iL + 1);
					if (badCharsForPlaceholder.Any(z => ph.Contains(z)) == false)
					{ result.Add(iL, ph); }
					iL = -1;
				}//if
			}//for

			return result;
		}//func


		///<summary>формат строки по объектам</summary> 
		public static string fmto(this string s, params object[] oo)
		{
			//получаем список плейс-холдеров
			var phS = s.getPlaceholders();
			if (phS == null) { return s; }

			#region заполн€ем значени€ плейс-холдеров
			IDictionary<string, string> values = new Dictionary<string, string>();
			string ph;
			string method;
			string[] args;
			string value;
			int iL, iR; //номер скобок
			foreach (var item in phS.Values.Distinct())
			{
				ph = item.Substring(1, item.Length - 2);//2="{}".Length
				method = null;
				args = null;
				iL = ph.IndexOf('(');
				if (iL >= 0)
				{
					iR = ph.IndexOf(')');
					method = ph.Substring(0, iL);
					args = (iR > iL + 1) ? ph.Substring(iL + 1, iR - iL - 1).Split(',') : null;
				}//if

				//обходим каждый объект из вход€щего списка
				//вдруг у кого-то есть нужное свойство или метод
				foreach (object o in oo)
				{
					if (method != null)
					{
						value = o.getMethodValue(method, args);
					}//if
					else
					{
						value = o.getPropertyValue(ph);
					}//else

					//нашлось - записываем и дальше не смотрим
					if (value != null)
					{
						values.Add(item, value);
						break;
					}//if
				}//for
			}//for
			#endregion

			#region формируем строку-результат
			string sPh;
			StringBuilder sb = new StringBuilder();
			int iPast = 0;//end of previous placeholder
			foreach (int iPh in phS.Keys)
			{
				sb.Append(s.Substring(iPast, iPh - iPast)); //before Ph
				sPh = phS[iPh]; //"{name}"
				sb.Append(values.ContainsKey(sPh) ? values[sPh] : string.Empty);
				iPast = iPh + sPh.Length;
			}//for
			sb.Append(s.Substring(iPast));
			#endregion

			return sb.ToString();
		}//function

		#region checks


		/// <summary>string not IsNullOrWhiteSpace</summary>
		public static bool notEmpty(this string s) { return !string.IsNullOrWhiteSpace(s); }
		///<summary>IsNullOrWhiteSpace</summary> 
		public static bool isEmpty(this string s) { return string.IsNullOrWhiteSpace(s); }
		///<summary>по умолчанию, когда пусто</summary> 
		public static string whenEmpty(this string s, string def)	{	return string.IsNullOrWhiteSpace(s) ? def : s; }
		///<summary>окавычить, оскобить</summary> 
		public static string quote(this string s, char ch)
		{
			return "{0}{1}{2}".fmt(ch, s, C.Right(ch));
		}//function

		///<summary>StartsWith (есть проверка на null)</summary>
		public static bool startsWithCI(this string s, string what)
		{
			return s == null ? false : s.StartsWith(what, StringComparison.OrdinalIgnoreCase);
		}//function
		 ///<summary>EndsWith (есть проверка на null)</summary>
		public static bool endsWithCI(this string s, string what)
		{
			return s == null ? false : s.EndsWith(what, StringComparison.OrdinalIgnoreCase);
		}//function

		/// <summary>Contains IgnoreCase</summary>
		public static bool containsCI(this string s, string what)
		{
			return s.IndexOf(what, StringComparison.OrdinalIgnoreCase) >= 0;
		}
		/// <summary>равно IgnoreCase</summary>
		public static bool equalCI(this string s, string what)
		{
			return string.Compare(s, what, true) == 0;
		}

		/// <summary>есть ли строка в массиве</summary>
		public static bool inList(this string s, params string[] ss) { return ss.Contains(s); }
		/// <summary>есть ли строка в массиве</summary>
		public static bool inList<T>(this T s, params T[] ss) { return ss.Contains(s); }
		#endregion

		#region substring
		/// <summary>берет кусок строки после <paramref name="Prefix"/>. ѕример: "Omnibus".after("b") = "us"</summary>
		public static string after(this string s, string Prefix)
		{
			int i = s.IndexOf(Prefix);
			if (i >= 0)
				return s.Substring(i + Prefix.Length);
			else
				return string.Empty;
		}//func

		/// <summary>берет кусок строки после <paramref name="Prefix"/>. ѕример: "Omnibus".after("b") = "us"</summary>
		public static string afterCI(this string s, string Prefix)
		{
			int i = s.IndexOf(Prefix, StringComparison.OrdinalIgnoreCase);
			if (i >= 0)
				return s.Substring(i + Prefix.Length);
			else
				return string.Empty;
		}//func

		/// <summary>берет кусок строки перед <paramref name="Suffix"/>. ѕример: "Omnibus".before("b") = "Omni"</summary>
		public static string before(this string s, string Suffix)
		{
			int i = s.IndexOf(Suffix);
			if (i > 0)
				return s.Substring(0, i);
			else
				return string.Empty;
		}//func

		/// <summary>берет кусок строки перед <paramref name="Suffix"/>. ѕример: "Omnibus".before("b") = "Omni"</summary>
		public static string beforeCI(this string s, string Suffix)
		{
			int i = s.IndexOf(Suffix, StringComparison.OrdinalIgnoreCase);
			if (i > 0)
				return s.Substring(0, i);
			else
				return string.Empty;
		}//func

		/// <summary>берет кусок строки между <paramref name="Prefix"/> и <paramref name="Suffix"/>. ѕример: "Omnibus".midst("O","b") = "mni"</summary>
		public static string midst(this string s, string Prefix, string Suffix)
		{
			int iPrefix = s.IndexOf(Prefix);
			int iSuffix = s.IndexOf(Suffix);
			if (iPrefix >= 0 && iSuffix > 0)
				return s.Substring(iPrefix + Prefix.Length, iSuffix - iPrefix - Prefix.Length);
			else if (iPrefix >= 0)
				return s.Substring(iPrefix + Prefix.Length);
			else if (iSuffix >= 0)
				return s.Substring(0, iSuffix);
			else
				return string.Empty;
		}//func

		/// <summary>берет кусок строки между <paramref name="Prefix"/> и <paramref name="Suffix"/>. ѕример: "Omnibus".midst("O","b") = "mni"</summary>
		public static string midstCI(this string s, string Prefix, string Suffix)
		{
			int iPrefix = s.IndexOf(Prefix, StringComparison.OrdinalIgnoreCase);
			int iSuffix = s.IndexOf(Suffix, StringComparison.OrdinalIgnoreCase);
			if (iPrefix >= 0 && iSuffix > 0)
				return s.Substring(iPrefix + Prefix.Length, iSuffix - iPrefix - Prefix.Length);
			else if (iPrefix >= 0)
				return s.Substring(iPrefix + Prefix.Length);
			else if (iSuffix >= 0)
				return s.Substring(0, iSuffix);
			else
				return string.Empty;
		}//func

		///<summary>замена символов. (smth, ms, MS) = SMth</summary> 
		public static string translate(this string s, string source, string destination)
		{
			StringBuilder result = new StringBuilder(s);
			int minLength = Math.Min(source.Length, destination.Length);
			for (int i = 0; i < minLength; i++)
			{
				result.Replace(source[i], destination[i]);
			}
			return result.ToString();
		}//function
		
		///<summary>левые ’ симоволов</summary> 
		public static string left(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) {return value;}
			maxLength = Math.Abs(maxLength);
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}
		#endregion

		#region add
		/// <summary>формирует Path из массива строк</summary>
		public static string addToPath(this string s, params string[] args)
		{
			foreach (string path in args)
			{
				s = System.IO.Path.Combine(s, path);
			}//for
			return s;
		}//func

		/// <summary>конкатенаци€ массива к строке</summary>
		public static string add(this string s, params object[] args)
		{
			StringBuilder sb = (s == null) ? new StringBuilder() : new StringBuilder(s);
			args.forEach(o => sb.Append(o));
			return sb.ToString();
		}//function

		/// <summary>конкатенаци€ к строке массива с разделителем</summary>
		public static string addDelim(this string s, string Delim, params object[] args)
		{
			if (!args.Any())
			{
				return s;
			}//if

			StringBuilder sb;
			if (s.isEmpty())
			{
				sb = new StringBuilder(args.First().ToString());
				args.Skip(1).forEach(o => sb.AppendFormat("{0}{1}", Delim, o));
			}//if
			else
			{
				sb = new StringBuilder(s);
				args.forEach(o => sb.AppendFormat("{0}{1}", Delim, o));
			}//else
			return sb.ToString();
		}//function
		
		/// <summary>конкатенаци€ к строке массива с разделителем Space</summary>
		public static string addSpace(this string s, params object[] args){	return s.addDelim(S.Space, args);	}//function
		/// <summary>конкатенаци€ к строке массива с разделителем «ап€та€</summary>
		public static string addComma(this string s, params object[] args) { return s.addDelim(S.Comma, args); }//function
		/// <summary>конкатенаци€ к строке текста с переносом строки из массива</summary>
		public static string addLine(this string s, params object[] args) { return s.addDelim(Environment.NewLine, args); }//function


		#endregion

		#region parse
		///<summary>see name</summary>
		public static DateTime parse(this string s, DateTime def, IFormatProvider formatProvider = null)
		{
			DateTime z;
			if (formatProvider == null)
			{
				return DateTime.TryParse(s, out z) ? z : def;
			}//if
			else
			{
				return DateTime.TryParse(s, formatProvider, DateTimeStyles.None, out z) ? z : def;
			}//else

		}//function

		private static readonly char[] ccPointAndComma = { C.Point, C.Comma };
		///<summary>see name</summary>
		public static decimal parseMoney(this string s)
		{
			int iPoint = s.IndexOfAny(ccPointAndComma);
			if (iPoint < 0)
			{
				return s.parseDecimal();
			}//if
			else if (iPoint >= 0)
			{
				string s2 = s.left(iPoint + 3);
				return s2.parseDecimal();
			}//if
			return 0M;
		}//function
		 ///<summary>see name</summary>
		public static Decimal parseDecimal(this string s)
		{
			Func<IEnumerable<char>, IEnumerable<char>> digits = (cc) => (cc.Where(ch => char.IsDigit(ch)));
			Func<IEnumerable<char>, int> chars2int = (cc) => {
				if (cc == null) {return 0;}
				var digitS = digits(cc);
				if (digitS.Any())
				{
					return Convert.ToInt32(new string(digitS.ToArray()));
				}//if
				else
				{
					return 0;
				}//else
			};

			int iLen = s.Length;
			if (iLen == 0) { return 0; }//if

			IEnumerable<char> ccBig, ccPart;
			int iSeparator = s.IndexOfAny(ccPointAndComma);
			if (iSeparator == 0)
			{
				ccBig = null;
				ccPart = s.Skip(1);
			}//if
			else if (iSeparator == iLen - 1 || iSeparator < 0)
			{
				ccBig = s;
				ccPart = null;
			}//if
			else
			{
				ccBig = s.Take(iSeparator);
				ccPart = s.Skip(iSeparator + 1);
			}//if

			int iBig = chars2int(ccBig);
			int iPart = chars2int(ccPart);

			Decimal result = iBig;
			if (iPart > 0)
			{
				result += (decimal)(iPart / (Math.Pow(10, digits(ccPart).Count())));
			}//if

			return result;
		}//function
		 
		///<summary>see name</summary>
		public static Decimal parse(this string s, Decimal def, IFormatProvider formatProvider = null)
		{
			Decimal z;
			if (formatProvider == null)
			{

				//NumberFormatInfo nf = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
				NumberFormatInfo nf = CultureInfo.InvariantCulture.NumberFormat;
				string s2 = s;
				if (nf.CurrencyGroupSeparator != S.Space && s.IndexOf(C.Space) >= 0) { s2 = s.Replace(S.Space, string.Empty); }
				if (nf.CurrencyDecimalSeparator == S.Point && s.IndexOf(C.Comma) >= 0) { s2 = s2.Replace(C.Comma, C.Point); }
				if (nf.CurrencyDecimalSeparator == S.Comma && s.IndexOf(C.Point) >= 0) { s2 = s2.Replace(C.Point, C.Comma); }

				bool OK = Decimal.TryParse(s2, out z);
				if (OK) { return z; }//if
				return def;
			}//if
			else
			{
				return Decimal.TryParse(s, NumberStyles.Number, formatProvider, out z) ? z : def;
			}//else
		}//function

		///<summary>see name</summary>
		public static int parse(this string s, int def) { int z; return int.TryParse(s, out z) ? z : def; }//function
		///<summary>see name</summary>
		public static string parse(this string s, string def) { return s ?? def; }//function
		#endregion

		#region split
		/// <summary>
		/// split : двоеточие
		/// </summary>
		public static string[] splitColon(this string s) { return s.Split(C.Colon); }//function
		/// <summary>
		/// split , зап€та€
		/// </summary>
		public static string[] splitComma(this string s) { return s.Split(C.Comma); }//function
		/// <summary>
		/// split ; точка с зап€той
		/// </summary>
		public static string[] splitSemicolon(this string s) { return s.Split(C.Semicolon); }//function
		/// <summary>
		/// split . точка
		/// </summary>
		public static string[] splitPoint(this string s) { return s.Split(C.Point); }//function
		/// <summary>
		/// split ѕ–ќЅ≈Ћ
		/// </summary>
		public static string[] splitSpace(this string s) { return s.Split(C.Space); }//function
		/// <summary>
		/// split TAB
		/// </summary>
		public static string[] splitTab(this string s) { return s.Split(C.Tab); }//function
		/// <summary>
		/// split line
		/// </summary>
		public static string[] splitLine(this string s) { return s.Split(Environment.NewLine.ToCharArray()); }//function
		/// <summary>
		/// split /
		/// </summary>
		public static string[] splitSlash(this string s) { return s.Split(C.Slash); }//function
		#endregion
	}//class
}//ns
