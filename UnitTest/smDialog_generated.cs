

// Generated 28.10.2015 2:06:55
//NO errors
//warnings
//Act=DoOne2 is never used

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDB;

namespace Onto
{

public enum smDialogState { First, Last, Some }
public enum smDialogPush { Start, Finish }

public sealed partial class smDialog
{
	public smDialog()
	{
		CurrentState = smDialogState.First;
	}//constructor

	public bool IsCheckOK {get; private set;}
	public bool IsActOK {get; private set;}
	public bool IsPushOK {get; private set;}
	public smDialogState CurrentState { get; private set; }

	//checks
	partial void checkIsUserHasRights();
partial void checkIsAllGood();

	//acts
	partial void actMake();
partial void actDoOne();
partial void actDoOne2();

public bool Push(smDialogPush push)
{
	switch (push)
	{
case smDialogPush.Start:
		
if (CurrentState == smDialogState.First)
{
	IsCheckOK = IsActOK = IsPushOK = false;
	checkIsAllGood(); if (!IsCheckOK) { break; }
checkIsUserHasRights(); if (!IsCheckOK) { break; }
	actMake(); if (!IsActOK) { break; }
	CurrentState = smDialogState.Last;
	IsPushOK = true;
}//if
break;
case smDialogPush.Finish:
		
if (CurrentState == smDialogState.Some)
{
	IsCheckOK = IsActOK = IsPushOK = false;
	
	actDoOne(); if (!IsActOK) { break; }
	CurrentState = smDialogState.Last;
	IsPushOK = true;
}//if
break;
	}
	return true;
}//function


}//class


}//namespace
