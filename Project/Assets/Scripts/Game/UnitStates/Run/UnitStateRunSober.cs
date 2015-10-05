using UnityEngine;
using System.Collections;

public class UnitStateRunSober : UnitStateRunBase 
{
	// ------------------------------------------------------------------------------------ //

	private float runSpeed = 2.0f;

	// ------------------------------------------------------------------------------------ //

	public sealed override void updateState () 
	{
		_unit.transform.position += _unit.transform.forward * Time.deltaTime * runSpeed;
	}

	// ------------------------------------------------------------------------------------ //
}
