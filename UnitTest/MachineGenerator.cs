

// Directory = D:\Repository\libz\UnitTest
// Machines.Count = 1
// Generated 03.11.2015 21:24:09
// Machine Dialog
//NO errors
//NO warnings

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDB;

namespace UnitTest
{

public enum smDialogSTATE { Created, Prepared, HasData }
public enum smDialogPUSH { Prepare, Select }
public enum smDialogACT { Connect, Disconnect, NONE }
public enum smDialogCHECK { IsOK, IsOnline, NONE }

///<summary>
///
///</summary>
/**/
public sealed partial class smDialog
{
	public smDialog()
	{
		CurrentState = smDialogSTATE.Created;
		LastState = CurrentState;
		BadAct = smDialogACT.NONE;
		BadCheck = smDialogCHECK.NONE;
	}//constructor

	public bool IsCheckOK {get; private set;}
	public bool IsActOK {get; private set;}
	public bool IsPushOK {get; private set;}
	public smDialogSTATE CurrentState { get; private set; }
	public smDialogSTATE LastState { get; private set; }
	public smDialogACT BadAct { get; private set; }
	public smDialogCHECK BadCheck { get; private set; }

	#region devices
///<summary></summary>
public DevOne dev1;
private DevOne get_dev1()
{
if (dev1 == null) { dev1=new DevOne(); }
return dev1;
}

///<summary></summary>
public DevTwo dev2;
private DevTwo get_dev2()
{
if (dev2 == null) { dev2=new DevTwo(); }
return dev2;
}

	#endregion

	#region checks
///<summary></summary>
void checkIsOK()
{
IsCheckOK = get_dev2().IsOK;
}

///<summary></summary>
void checkIsOnline()
{
IsCheckOK = get_dev1().IsOnline;
}

	#endregion
	
#region acts
///<summary></summary>
void actConnect()
{
get_dev1().IsOnline=true;
IsActOK = true;
}

///<summary></summary>
void actDisconnect()
{
get_dev1().IsOnline=false;
IsActOK = true;
}

#endregion

public bool Push(smDialogPUSH push)
{
	IsPushOK = false;
	switch (push)
	{
case smDialogPUSH.Prepare:
		
if (CurrentState == smDialogSTATE.Created)
{
	IsCheckOK = false; checkIsOnline(); if (!IsCheckOK) { BadCheck = smDialogCHECK.IsOnline; break; }
IsCheckOK = false; checkIsOK(); if (!IsCheckOK) { BadCheck = smDialogCHECK.IsOK; break; }
	IsActOK = false; actConnect(); if (!IsActOK) { BadAct = smDialogACT.Connect; break; }
	CurrentState = smDialogSTATE.Prepared;
	IsPushOK = true;
}//if
break;
case smDialogPUSH.Select:
		
if (CurrentState == smDialogSTATE.Prepared)
{
	IsCheckOK = false; checkIsOK(); if (!IsCheckOK) { BadCheck = smDialogCHECK.IsOK; break; }
	IsActOK = false; actDisconnect(); if (!IsActOK) { BadAct = smDialogACT.Disconnect; break; }
	CurrentState = smDialogSTATE.HasData;
	IsPushOK = true;
}//if
break;
	}
	return IsPushOK;
}//function


}//class


}//namespace



