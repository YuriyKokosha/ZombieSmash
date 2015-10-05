using UnityEngine;
using System.Collections;

public abstract class UnitManBase : UnitBase 
{
	public sealed override void createUnitModel () 
	{
		GameObject modelGameObject = Instantiate(ResourcesBase.load("Prefabs/Units/Human"),
		                                         new Vector3 (0,0,0),
		                                         Quaternion.Euler(0,0,0)) as GameObject;
		modelGameObject.transform.parent = transform;
		modelGameObject.name = "Model";
		
		base.setUnitModel(UnitModelBase.createNewInstance(modelGameObject));

		base.addCapsuleCollider(0.5f,2.0f,new Vector3 (0.0f,1.0f,0.0f));
		base.addRigidbody();
	}
}
