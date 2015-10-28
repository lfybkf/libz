

// Generated 28.10.2015 14:26:08
// Machine Dialog
//NO errors
//warnings
//Act=DoOne2 is never used

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDB;

namespace UnitTest
{

public enum smDialogSTATE { First, Last, Midst }
public enum smDialogPUSH { Start, Finish }

///<summary>
///Show dialog
///</summary>
/*me:Диалог с пользователем*/
public sealed partial class smDialog
{
	public smDialog()
	{
		CurrentState = smDialogSTATE.First;
	}//constructor

	public bool IsCheckOK {get; private set;}
	public bool IsActOK {get; private set;}
	public bool IsPushOK {get; private set;}
	public smDialogSTATE CurrentState { get; private set; }

	//checks
	///<summary>test user rights</summary>
 partial void checkIsUserHasRights();
///<summary></summary>
 partial void checkIsAllGood();

	//acts
	///<summary></summary>
 partial void actMake();
///<summary></summary>
 partial void actDoOne();
///<summary></summary>
 partial void actDoOne2();

public bool Push(smDialogPUSH push)
{
	IsPushOK = false;
	switch (push)
	{
case smDialogPUSH.Start:
		
if (CurrentState == smDialogSTATE.First)
{
	IsCheckOK = false; checkIsAllGood(); if (!IsCheckOK) { break; }
IsCheckOK = false; checkIsUserHasRights(); if (!IsCheckOK) { break; }
	IsActOK = false; actMake(); if (!IsActOK) { break; }
	CurrentState = smDialogSTATE.Last;
	IsPushOK = true;
}//if
break;
case smDialogPUSH.Finish:
		
if (CurrentState == smDialogSTATE.Midst)
{
	
	IsActOK = false; actDoOne(); if (!IsActOK) { break; }
	CurrentState = smDialogSTATE.Last;
	IsPushOK = true;
}//if
break;
	}
	return IsPushOK;
}//function


}//class


}//namespace



