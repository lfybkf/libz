using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BDB
{
	public static class StringExtension
	{
		/// <summary>
		/// формат строки
		/// usage: "Value1={0}, Value2={1}".fmt(Value1, Value2)
		/// </summary>
		/// <param name="s"></param>
		/// <param name="oo"></param>
		/// <returns></returns>
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }//func

		/// <summary>
		/// from "asd{name}qwer{age}ty" get 3,"{name}" ;14,"{age}"
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cL"></param>
		/// <param name="cR"></param>
		/// <returns></returns>
		internal static IDictionary<int, string> getPlaceholders(this string s, char cL = '{', char cR = '}')
		{
			if (s.IndexOf(cL) < 0) { return null; }//if

			IDictionary<int, string> result = new Dictionary<int, string>();
			char c;
			int iL = -1;
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
					result.Add(iL, s.Substring(iL, i - iL + 1));
					iL = -1;
				}//if
			}//for

			return result;
		}//func


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


		/// <summary>string not IsNullOrWhiteSpace</summary>
		public static bool notEmpty(this string s) { return !string.IsNullOrWhiteSpace(s); }
		public static bool isEmpty(this string s) { return string.IsNullOrWhiteSpace(s); }
		public static string whenEmpty(this string s, string def)
		{
			return string.IsNullOrWhiteSpace(s) ? def : s;
		}



		/// <summary>
		/// Contains IgnoreCase
		/// </summary>
		/// <returns></returns>
		public static bool containsCI(this string s, string what)
		{
			return s.IndexOf(what, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public static bool equalCI(this string s, string what)
		{
			return string.Compare(s, what, true) == 0;
		}

		/// <summary>
		/// есть ли строка в массиве
		/// </summary>
		/// <param name="s"></param>
		/// <param name="ss">массив, в котором искать</param>
		/// <returns></returns>
		public static bool inList(this string s, params string[] ss) { return ss.Contains(s); }//func

		#region substring
		/// <summary>
		/// берет кусок строки после <paramref name="Prefix"/>
		/// ѕример: "Omnibus".after("b") = "us"
		/// </summary>
		/// <param name="s"></param>
		/// <param name="Prefix">искома€ строка</param>
		/// <returns></returns>
		public static string after(this string s, string Prefix)
		{
			int i = s.IndexOf(Prefix);
			if (i >= 0)
				return s.Substring(i + Prefix.Length);
			else
				return string.Empty;
		}//func

		public static string before(this string s, string Suffix)
		{
			int i = s.IndexOf(Suffix);
			if (i > 0)
				return s.Substring(0, i);
			else
				return string.Empty;
		}//func

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


		public static string translate(this string input, string source, string destination)
		{
			StringBuilder result = new StringBuilder(input);
			int minLength = Math.Min(source.Length, destination.Length);
			for (int i = 0; i < minLength; i++)
			{
				result.Replace(source[i], destination[i]);
			}
			return result.ToString();
		}//function
		
		public static string left(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) {return value;}
			maxLength = Math.Abs(maxLength);
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}
		#endregion

		#region add
		/// <summary>
		/// формирует Path из массива строк
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string addToPath(this string s, params string[] args)
		{
			foreach (string path in args)
			{
				s = System.IO.Path.Combine(s, path);
			}//for
			return s;
		}//func

		/// <summary>
		/// конкатенаци€ массива к строке
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string add(this string s, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.Append(o));
			return sb.ToString();
		}//function

		/// <summary>
		/// конкатенаци€ к строке текста с переносом строки из массива
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string addLine(this string s, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.AppendFormat("{0}{1}", Environment.NewLine, o));
			return sb.ToString();
		}//function

		/// <summary>конкатенаци€ к строке массива с разделителем</summary>
		public static string addDelim(this string s, string Delim, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.AppendFormat("{0}{1}", Delim, o));
			return sb.ToString();
		}//function
		
		/// <summary>конкатенаци€ к строке массива с разделителем Space</summary>
		public static string addSpace(this string s, params object[] args)
		{
			return s.addDelim(" ", args);
		}//function

		#endregion

		#region parse
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

		private static readonly char[] ccPointAndComma = { ',', '.' };
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

		public static Decimal parse(this string s, Decimal def, IFormatProvider formatProvider = null)
		{
			Decimal z;
			if (formatProvider == null)
			{

				//NumberFormatInfo nf = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
				NumberFormatInfo nf = CultureInfo.InvariantCulture.NumberFormat;
				string s2 = s;
				if (nf.CurrencyGroupSeparator != sSpace && s.IndexOf(cSpace) >= 0) { s2 = s.Replace(sSpace, string.Empty); }
				if (nf.CurrencyDecimalSeparator == sPoint && s.IndexOf(cComma) >= 0) { s2 = s2.Replace(cComma, cPoint); }
				if (nf.CurrencyDecimalSeparator == sComma && s.IndexOf(cPoint) >= 0) { s2 = s2.Replace(cPoint, cComma); }

				bool OK = Decimal.TryParse(s2, out z);
				if (OK) { return z; }//if
				return def;
			}//if
			else
			{
				return Decimal.TryParse(s, NumberStyles.Number, formatProvider, out z) ? z : def;
			}//else
		}//function

		public static int parse(this string s, int def) { int z; return int.TryParse(s, out z) ? z : def; }//function
		public static string parse(this string s, string def) { return s ?? def; }//function
		#endregion

		#region split
		const char cTab = '\t';
		const char cSpace = ' '; const string sSpace = " ";
		const char cPoint = '.'; const string sPoint = ".";
		const char cComma = ','; const string sComma = ",";
		const char cColon = ':';
		const char cSemicolon = ';';
		const char cSlash = '/';

		/// <summary>
		/// split : двоеточие
		/// </summary>
		public static string[] splitColon(this string s) { return s.Split(cColon); }//function
		/// <summary>
		/// split , зап€та€
		/// </summary>
		public static string[] splitComma(this string s) { return s.Split(cComma); }//function
		/// <summary>
		/// split ; точка с зап€той
		/// </summary>
		public static string[] splitSemicolon(this string s) { return s.Split(cSemicolon); }//function
		/// <summary>
		/// split . точка
		/// </summary>
		public static string[] splitPoint(this string s) { return s.Split(cPoint); }//function
		/// <summary>
		/// split ѕ–ќЅ≈Ћ
		/// </summary>
		public static string[] splitSpace(this string s) { return s.Split(cSpace); }//function
		/// <summary>
		/// split TAB
		/// </summary>
		public static string[] splitTab(this string s) { return s.Split(cTab); }//function
		/// <summary>
		/// split line
		/// </summary>
		public static string[] splitLine(this string s) { return s.Split(Environment.NewLine.ToCharArray()); }//function
		/// <summary>
		/// split /
		/// </summary>
		public static string[] splitSlash(this string s) { return s.Split('/'); }//function
		#endregion
	}//class
}//ns
