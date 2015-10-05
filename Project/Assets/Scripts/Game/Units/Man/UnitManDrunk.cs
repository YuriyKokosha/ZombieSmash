using UnityEngine;
using System.Collections;

public class UnitManDrunk : UnitManBase 
{
	public sealed override void initialize () 
	{ 
		base.createUnitModel();
	}
	
	public sealed override void setStateRun () {
		base.setUnitState(new UnitStateRunDrunk ());
	}
	
	public sealed override void setStateDead () {
		base.setUnitState(new UnitStateDead ());
	}
	
	public sealed override void updateUnit () 
	{
		base.updateState();
	}
}
