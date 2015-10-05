using UnityEngine;
using System.Collections;

public class UnitFactory : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //

	public static UnitBase createNewUnit (UnitType unitType) 
	{
		GameObject unitGameObject = new GameObject ("Unit" + unitType.ToString());
		unitGameObject.tag = UnitBase.UNIT_TAG;

		UnitBase unitBase = null;

		if (unitType == UnitType.ZombieSober) {
			unitBase = unitGameObject.AddComponent<UnitZombieSober>();
		}
		else if (unitType == UnitType.ZombieDrunk) {
			unitBase = unitGameObject.AddComponent<UnitZombieDrunk>();
		}
		else if (unitType == UnitType.ManDrunk) {
			unitBase = unitGameObject.AddComponent<UnitManDrunk>();
		}
		else {
			SLog.logError("UnitFactory createNewUnit(): unknown type == " + unitType.ToString());
		}

		if (unitBase != null) 
		{
			unitBase.setUnitType(unitType);
			unitBase.initialize();
		}

		return unitBase;
	}

	// ------------------------------------------------------------------------------------ //

	public static UnitType getRandomUnitType () 
	{
		if (Probability.getProbability(20)) {
			return UnitType.ManDrunk;
		}
		else if (Probability.getProbability(50)) {
			return UnitType.ZombieSober;
		}
		else {
			return UnitType.ZombieDrunk;
		}
	}

	// ------------------------------------------------------------------------------------ //
}
