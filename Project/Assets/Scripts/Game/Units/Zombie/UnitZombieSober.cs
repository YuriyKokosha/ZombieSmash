using UnityEngine;
using System.Collections;

public class UnitZombieSober : UnitZombieBase 
{
	public sealed override void initialize () 
	{ 
		base.createUnitModel();
	}

	public sealed override void setStateRun () {
		base.setUnitState(new UnitStateRunSober ());
	}

	public sealed override void setStateDead () {
		base.setUnitState(new UnitStateDead ());
	}

	public sealed override void updateUnit () 
	{
		base.updateState();
	}
}
