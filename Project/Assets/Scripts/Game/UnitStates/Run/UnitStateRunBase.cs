using UnityEngine;
using System.Collections;

public abstract class UnitStateRunBase : UnitStateBase, IUnitCollisionObserver
{
	// ------------------------------------------------------------------------------------ //

	public override void initialize (UnitBase unit) 
	{ 
		base.initialize(unit);
		unit.getUnitModel().startAnimation(UnitModelAnimationType.Run);
		unit.registerObserver((IUnitCollisionObserver) this);
	}

	public override void destroyState () 
	{
		_unit.removeObserver((IUnitCollisionObserver) this);
		base.destroyState();
	}

	// ------------------------------------------------------------------------------------ //

	public virtual void onUnitCollision (GameObject other)
	{
		if (other.tag.Equals("LevelEnd"))
		{
			_unit.notifyUnitRunAwayObservers();
			_unit.destroyUnit();
		}
		else if (other.tag.Equals("Bomb"))
		{
			_unit.notifyUnitDeadObservers();
			_unit.setStateDead();
		}
	}

	// ------------------------------------------------------------------------------------ //

}
