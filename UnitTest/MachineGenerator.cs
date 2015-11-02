

// Directory = D:\Repository\libz\UnitTest
// Machines.Count = 1
// Generated 03.11.2015 1:15:22
// Machine Dialog
//NO errors
//warnings
//Act=One is never used

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

///<summary>
///
///</summary>
/**/
public sealed partial class smDialog
{
	public smDialog()
	{
		CurrentState = smDialogSTATE.Created;
	}//constructor

	public bool IsCheckOK {get; private set;}
	public bool IsActOK {get; private set;}
	public bool IsPushOK {get; private set;}
	public smDialogSTATE CurrentState { get; private set; }

	#region devices
///<summary></summary>
public Dev1 dev1;
private Dev1 get_dev1()
{
if (dev1 == null) { dev1=new Dev1(); }
return dev1;
}

///<summary></summary>
public Dev2 dev2;
private Dev2 get_dev2()
{
return dev2;
}

	#endregion

	#region checks
///<summary></summary>
void checkIsOnline()
{
IsCheckOK = get_dev1().IsOnline;
}

///<summary></summary>
void checkIsOK()
{
IsCheckOK = get_dev2().IsOK;
}

	#endregion
	
#region acts
///<summary></summary>
void actDisconnect()
{
get_dev1().Online = false;
IsActOK = true;
}

///<summary></summary>
void actOne()
{
get_dev2().Do(14);
IsActOK = true;
}

///<summary></summary>
void actConnect()
{
get_dev1().Online = true;
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
	IsCheckOK = false; checkIsOnline(); if (!IsCheckOK) { break; }
IsCheckOK = false; checkIsOK(); if (!IsCheckOK) { break; }
	IsActOK = false; actConnect(); if (!IsActOK) { break; }
	CurrentState = smDialogSTATE.Prepared;
	IsPushOK = true;
}//if
break;
case smDialogPUSH.Select:
		
if (CurrentState == smDialogSTATE.Prepared)
{
	IsCheckOK = false; checkIsOK(); if (!IsCheckOK) { break; }
	IsActOK = false; actDisconnect(); if (!IsActOK) { break; }
	CurrentState = smDialogSTATE.HasData;
	IsPushOK = true;
}//if
break;
	}
	return IsPushOK;
}//function


}//class


}//namespace



