using UnityEngine;
using System.Collections;

public class UnitStateDead : UnitStateBase 
{
	// ------------------------------------------------------------------------------------ //

	float life = 2.0f;	// destroy unit after 2 seconds

	// ------------------------------------------------------------------------------------ //

	public sealed override void initialize (UnitBase unit) 
	{ 
		base.initialize(unit);
		_unit.getUnitModel().startAnimation(UnitModelAnimationType.Die);
	}

	public sealed override void updateState () 
	{
		life -= Time.deltaTime;
		if (life < 0) {
			_unit.destroyUnit();
		}
	}

	public sealed override void destroyState () 
	{
		base.destroyState();
	}

	// ------------------------------------------------------------------------------------ //
}
