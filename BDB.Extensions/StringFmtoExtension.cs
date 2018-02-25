using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>Параметры для fmto</summary>
	public sealed class FmtoParams
	{
		///<summary>режим обработки пустых Phalue</summary> 
		public enum ModeAbsent {
			///<summary>заменять пустой строкой</summary> 
			EMPTY,
			///<summary>оставлять как есть</summary> 
			SAME
		}

		///<summary>значение для placeHolder по умолчанию (если значение не найдено)</summary> 
		public ModeAbsent mode = ModeAbsent.EMPTY;
		///<summary>скобка</summary> 
		public char brace { get; set; } = C.LBrace;
		///<summary>параметры</summary> 
		public object[] parameters { get; set; }
		///<summary>добавить параметры</summary> 
		public FmtoParams withOO(params object[] oo) {
			this.parameters = oo;
			return this;
		}
	}//class

	///<summary>StringFmtoExtension</summary> 
	public static class StringFmtoExtension
	{
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
			FmtoParams fmtoParams = new FmtoParams { parameters = oo };
			return s._fmto(fmtoParams);
		}

		///<summary>формат строки по объектам. Phalue=SAME</summary>
		public static string fmts(this string s, params object[] oo)
		{
			FmtoParams fmtoParams = new FmtoParams { parameters = oo, mode= FmtoParams.ModeAbsent.SAME };
			return s._fmto(fmtoParams);
		}

		///<summary>формат строки по объектам</summary>
		public static string fmtoe(this string s, FmtoParams fmtoParams, params object[] oo)
		{
			fmtoParams.parameters = oo;
			return s._fmto(fmtoParams);
		}

		///<summary>формат строки по объектам</summary>
		public static string fmtoe(this string s, FmtoParams fmtoParams) => s._fmto(fmtoParams);

		private static string _fmto(this string s, FmtoParams fmtoParams)
		{
			//получаем список плейс-холдеров
			char lbrace = fmtoParams.brace;
			char rbrace = C.Right(fmtoParams.brace);
			var phS = s.getPlaceholders(cL:lbrace, cR:rbrace);
			if (phS == null) { return s; }

			#region находим значения плейс-холдеров
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

				//обходим каждый объект из входящего списка
				//вдруг у кого-то есть нужное свойство или метод
				foreach (object o in fmtoParams.parameters)
				{
					if (method != null) {
						value = o.getMethodValue(method, args);
					}	else {
						value = o.getPropertyValue(ph);
					}

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
			string phalue;
			foreach (int iPh in phS.Keys)
			{
				sb.Append(s.Substring(iPast, iPh - iPast)); //before Ph
				sPh = phS[iPh]; //"{name}"
				if (fmtoParams.mode == FmtoParams.ModeAbsent.SAME)
					phalue = sPh;// $"{lbrace}{sPh}{rbrace}";
				else
					phalue = string.Empty;
				sb.Append(values.ContainsKey(sPh) ? values[sPh] : phalue);
				iPast = iPh + sPh.Length;
			}//for
			sb.Append(s.Substring(iPast));
			#endregion

			return sb.ToString();
		}//function

	}
}
