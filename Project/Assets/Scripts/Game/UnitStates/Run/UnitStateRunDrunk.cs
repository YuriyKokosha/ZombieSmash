using UnityEngine;
using System.Collections;

public class UnitStateRunDrunk : UnitStateRunBase
{
	// ------------------------------------------------------------------------------------ //

	public enum DirectionType {
		Left,
		Right,
		Forward
	}

	// ------------------------------------------------------------------------------------ //

	private DirectionType currentDirectionType = DirectionType.Forward;

	private float runSpeed = 2.0f;
	private float rotateAngle = 30.0f;

	private float changeDirectionTime = 2.0f;
	private int changeDirectionProbability = 50;
	private int changeDirectionProbabilityDelta = 40;

	private float nextTime;

	// ------------------------------------------------------------------------------------ //

	public sealed override void updateState () 
	{
		// change unit position
		_unit.transform.position += _unit.transform.forward * Time.deltaTime * runSpeed;

		// check next change direction time
		if (changeDirectionProbability > 0) 
		{
			nextTime -= Time.deltaTime;
			if (nextTime < 0) {
				nextTime = changeDirectionTime;
				if (Probability.getProbability(changeDirectionProbability)) {
					changeDirectionProbability -= changeDirectionProbabilityDelta;
					changeDirectionRandom();
				}
			}
		}
	}

	public sealed override void onUnitCollision (GameObject other)
	{
		base.onUnitCollision(other);

		if (other.tag.Equals("LevelBorder"))
		{
			float angle = 0;
			
			if (currentDirectionType == DirectionType.Left) {
				currentDirectionType = DirectionType.Right;
				angle = -rotateAngle*2;
			}
			else if (currentDirectionType == DirectionType.Right) {
				currentDirectionType = DirectionType.Left;
				angle = rotateAngle*2;
			}
			
			rotateUnit(angle);
		}
	}

	// ------------------------------------------------------------------------------------ //

	private void changeDirectionRandom ()
	{
		float angle = 0;

		if (currentDirectionType == DirectionType.Forward) 
		{
			if (Probability.getProbability(50)) {
				currentDirectionType = DirectionType.Left;
				angle = rotateAngle;
			}
			else {
				currentDirectionType = DirectionType.Right;
				angle = -rotateAngle;
			}
		}
		else if (currentDirectionType == DirectionType.Left) 
		{
			currentDirectionType = DirectionType.Forward;
			angle = -rotateAngle;
		}
		else if (currentDirectionType == DirectionType.Right) 
		{
			currentDirectionType = DirectionType.Forward;
			angle = rotateAngle;
		}

		rotateUnit(angle);
	}

	private void rotateUnit (float angle) 
	{
		_unit.transform.RotateAround(_unit.transform.position,
		                             _unit.transform.up,
		                             angle);
	}

	// ------------------------------------------------------------------------------------ //
}
