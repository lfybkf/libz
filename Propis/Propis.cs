using System;
using System.Text;

namespace BDB
{
	//==================
	public enum PadezhN : int { Odin = 0, Dva = 1, Mnogo = 2 };

	public class Propis
	{
		//members
		private static string[] ssOnes;
		private static string[] ssDecades;
		private static string[] ssHundreds;
		private static string[] ssBigs;
		private static StringBuilder SB3;
		private static StringBuilder SB;
		private static readonly string sp = " ";
		private static readonly string spp = "  ";
		private const int BILLION = 1000000000;
		private const int MILLION = 1000000;
		private const int THOUSAND = 1000;
		private const int HUNDRED = 100;
		private const int TEN = 10;
		private const int TWENTY = 20;


		//constructor
		static Propis()
		{
			ssOnes = new string[] {string.Empty, string.Empty, string.Empty
										, "три", "четыре", "пять"
										, "шесть", "семь", "восемь", "девять", "десять"
										, "одинадцать", "двенадцать", "тринадцать", "четырнадцать"
										, "пятнадцать", "шестнадцать", "семнадцать"
										, "восемнадцать", "девятнадцать"};
			ssDecades = new string[] {"двадцать", "тридцать", "сорок", "пятьдесят", 
                   "шестьдесят", "семьдесят", "восемьдесят", "девяносто"};
			ssHundreds = new string[] {"сто", "двести", "триста", "четыреста", "пятьсот", 
                   "шестьсот", "семьсот", "восемьсот", "девятьсот"};
			ssBigs = new string[] {"тысяча", "тысячи", "тысяч",
                   "миллион", "миллиона", "миллионов",
                   "миллиард", "миллиарда", "миллиардов"};

			Pol = true;
			SB3 = new StringBuilder(50);
			SB = new StringBuilder(150);
		}//function

		public static string Go3(int aI, bool aPol)
		{
			//PREPARE
			aI = aI % THOUSAND;
			Pol = aPol;
			SB3.Remove(0, SB3.Length);

			//VAR
			int iD, iE;

			//hundreds
			if (aI >= HUNDRED)
			{
				SB3.Append(ssHundreds[aI / HUNDRED - 1]);
				SB3.Append(sp);
			} //if

			//other
			iE = aI % HUNDRED;
			if (iE >= TWENTY)
			{
				iD = iE / TEN;
				iE = iE % TEN;
				SB3.Append(ssDecades[iD - 2]);
				SB3.Append(sp);
				SB3.Append(ssOnes[iE]);
			} //if
			else
			{
				SB3.Append(ssOnes[iE]);
				SB3.Append(sp);
			}

			return SB3.ToString();
		}
		//============================================

		public static string Go(long aL) { return Go(aL, true); }//funciton

		public static string Go(long aL, bool aPol)
		{
			SB.Remove(0, SB.Length);
			int i;
			if (aL >= BILLION)
			{
				i = (int)(aL / BILLION);
				SB.Append(Go3(i, true));
				SB.Append(sp);
				SB.Append(ssBigs[6 + (int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % BILLION;
			}

			if (aL >= MILLION)
			{
				i = (int)(aL / MILLION);
				SB.Append(Go3(i, true));
				SB.Append(sp);
				SB.Append(ssBigs[3 + (int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % MILLION;
			}

			if (aL >= THOUSAND)
			{
				i = (int)(aL / THOUSAND);
				SB.Append(Go3(i, false));
				SB.Append(sp);
				SB.Append(ssBigs[(int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % THOUSAND;
			}

			SB.Append(Go3((int)aL, aPol));

			SB.Replace(spp, sp);
			SB.Replace(spp, sp);
			return SB.ToString();
		}
		//============================================
		public static PadezhN Padezh(long aL)
		{
			int i = (int)(aL % HUNDRED);
			PadezhN Ret = PadezhN.Mnogo;
			if (i < 5 || i > TWENTY)
			{
				switch (i % TEN)
				{
					case 1: Ret = PadezhN.Odin; break;
					case 2: goto case 4;
					case 3: goto case 4;
					case 4: Ret = PadezhN.Dva; break;
				}//switch
			}//if
			return Ret;
		}
		//============================================


		//properties
		private static bool Pol
		{
			set
			{
				if (value)	{	ssOnes[1] = "один";	ssOnes[2] = "два";}
				else	{ssOnes[1] = "одна"; ssOnes[2] = "две";}
			}//set
		}
	}//class
	//==================
}//namespace
