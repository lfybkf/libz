

// Directory = D:\Repository\libz\UnitTest
// Machines.Count = 1
// Generated 06.11.2015 0:04:58
// Machine Dialog
// States.Count = 3
// Transitions.Count = 3
// Pushes.Count = 5
// Checks.Count = 2
// Act.Count = 2
// Devices.Count = 2
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
public enum smDialogPUSH { Prepare, Puush1, Select, Puush2, Puush3 }
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

public IEnumerable<smDialogPUSH> availablePushes
{ get {
	IEnumerable<smDialogPUSH> result = null;
	switch (CurrentState)
	{

case smDialogSTATE.Created:
result = new smDialogPUSH[] { smDialogPUSH.Prepare, smDialogPUSH.Puush1 };
break;
case smDialogSTATE.Prepared:
result = new smDialogPUSH[] { smDialogPUSH.Select, smDialogPUSH.Puush2 };
break;
case smDialogSTATE.HasData:
result = new smDialogPUSH[] { smDialogPUSH.Puush3 };
break;
	}//switch
return result;
}}

public bool Check(smDialogPUSH push)
{
	BadCheck = smDialogCHECK.NONE;
	IsCheckOK = true;
	switch (push)
	{
case smDialogPUSH.Prepare:
if (CurrentState == smDialogSTATE.Created)
{
	IsCheckOK = false; checkIsOnline(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOnline; break; }
IsCheckOK = false; checkIsOK(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOK; break; }
}//if
break;
//Prepare

case smDialogPUSH.Puush1:
if (CurrentState == smDialogSTATE.Created)
{
	IsCheckOK = false; checkIsOnline(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOnline; break; }
IsCheckOK = false; checkIsOK(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOK; break; }
}//if
break;
//Puush1

case smDialogPUSH.Select:
if (CurrentState == smDialogSTATE.Prepared)
{
	IsCheckOK = false; checkIsOK(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOK; break; }
}//if
break;
//Select

case smDialogPUSH.Puush2:
if (CurrentState == smDialogSTATE.Prepared)
{
	IsCheckOK = false; checkIsOK(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOK; break; }
}//if
break;
//Puush2

case smDialogPUSH.Puush3:
if (CurrentState == smDialogSTATE.HasData)
{
	IsCheckOK = false; checkIsOnline(); if (IsCheckOK == false) { BadCheck = smDialogCHECK.IsOnline; break; }
}//if
break;
//Puush3

	}

	return IsCheckOK;
}//function

public bool Push(smDialogPUSH push)
{
	BadAct = smDialogACT.NONE;
	IsPushOK = Check(push);
	if (IsPushOK == false) {return IsPushOK;}

	switch (push)
	{
case smDialogPUSH.Prepare:
if (CurrentState == smDialogSTATE.Created)
{
	IsPushOK = false;
	IsActOK = false; actConnect(); if (!IsActOK) { BadAct = smDialogACT.Connect; break; }
	CurrentState = smDialogSTATE.Prepared;
	IsPushOK = true;
}//if
break;
//Prepare

case smDialogPUSH.Puush1:
if (CurrentState == smDialogSTATE.Created)
{
	IsPushOK = false;
	IsActOK = false; actConnect(); if (!IsActOK) { BadAct = smDialogACT.Connect; break; }
	CurrentState = smDialogSTATE.Prepared;
	IsPushOK = true;
}//if
break;
//Puush1

case smDialogPUSH.Select:
if (CurrentState == smDialogSTATE.Prepared)
{
	IsPushOK = false;
	IsActOK = false; actDisconnect(); if (!IsActOK) { BadAct = smDialogACT.Disconnect; break; }
	CurrentState = smDialogSTATE.HasData;
	IsPushOK = true;
}//if
break;
//Select

case smDialogPUSH.Puush2:
if (CurrentState == smDialogSTATE.Prepared)
{
	IsPushOK = false;
	IsActOK = false; actDisconnect(); if (!IsActOK) { BadAct = smDialogACT.Disconnect; break; }
	CurrentState = smDialogSTATE.HasData;
	IsPushOK = true;
}//if
break;
//Puush2

case smDialogPUSH.Puush3:
if (CurrentState == smDialogSTATE.HasData)
{
	IsPushOK = false;
	
	CurrentState = smDialogSTATE.Prepared;
	IsPushOK = true;
}//if
break;
//Puush3

	}
	return IsPushOK;
}//function


}//class


}//namespace



