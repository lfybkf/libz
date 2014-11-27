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
		private static string sp;

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
			sp = " ";
		}

		//func
		public static string Go3(int aI, bool aPol)
		{
			//PREPARE
			aI = aI % 1000;
			Pol = aPol;
			SB3.Remove(0, SB3.Length);

			//VAR
			int iD, iE;

			//hundreds
			if (aI >= 100)
			{
				SB3.Append(ssHundreds[aI / 100 - 1]);
				SB3.Append(sp);
			} //if

			//other
			iE = aI % 100;
			if (iE >= 20)
			{
				iD = iE / 10;
				iE = iE % 10;
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

		public static string Go(long aL, bool aPol)
		{
			SB.Remove(0, SB.Length);
			int i;
			int i9 = 1000000000;
			int i6 = 1000000;
			int i3 = 1000;
			if (aL >= i9)
			{
				i = (int)(aL / i9);
				SB.Append(Go3(i, true));
				SB.Append(sp);
				SB.Append(ssBigs[6 + (int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % i9;
			}

			if (aL >= i6)
			{
				i = (int)(aL / i6);
				SB.Append(Go3(i, true));
				SB.Append(sp);
				SB.Append(ssBigs[3 + (int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % i6;
			}

			if (aL >= i3)
			{
				i = (int)(aL / i3);
				SB.Append(Go3(i, false));
				SB.Append(sp);
				SB.Append(ssBigs[(int)Padezh(i)]);
				SB.Append(sp);
				aL = aL % i3;
			}

			SB.Append(Go3((int)aL, aPol));

			string spp = "  ";
			SB.Replace(spp, sp);
			SB.Replace(spp, sp);
			return SB.ToString();
		}
		//============================================
		public static PadezhN Padezh(long aL)
		{
			int i = (int)(aL % 100);
			PadezhN Ret = PadezhN.Mnogo;
			if (i < 5 || i > 20)
			{
				switch (i % 10)
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
