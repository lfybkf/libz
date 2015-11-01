

// Generated 01.11.2015 13:33:27
// Machine Dialog
//NO errors
//warnings
//Act=DoOne2 is never used
//Check=IsDev2 is not used
//Check=IsFunc is not used
//Device=dev3 is never used

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

	#region devices
///<summary>some dev1</summary>
public Dev1 dev1;
private Dev1 get_dev1()
{
if (dev1 == null) { dev1=new Dev1(); }
return dev1;
}

///<summary>some dev2</summary>
public Dev2 dev2;
private Dev2 get_dev2()
{
if (dev2 == null) { dev2=new Dev2(); }
return dev2;
}

///<summary>another dev2</summary>
public Dev2 dev3;
private Dev2 get_dev3()
{
if (dev3 == null) { dev3=Dev2.Instance; }
return dev3;
}

	#endregion

	#region checks
///<summary>test user rights</summary>
void checkIsUserHasRights()
{
IsCheckOK = get_dev1().IsOK;
}

///<summary></summary>
void checkIsAllGood()
{
IsCheckOK = get_dev2().IsOnline;
}

///<summary></summary>
void checkIsDev2()
{
IsCheckOK = dev2 != null;
}

///<summary></summary>
partial void checkIsFunc();

	#endregion
	
#region acts
///<summary></summary>
void actMake()
{
get_dev2().Offline();
IsActOK = true;
}

///<summary></summary>
partial void actDoOne();

///<summary></summary>
partial void actDoOne2();

#endregion

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



