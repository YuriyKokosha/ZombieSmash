using UnityEngine;
using System.Collections;

public class UnitModelBase : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //
	
	public static UnitModelBase createNewInstance (GameObject unitModelGameObject) 
	{
		UnitModelBase unitModel = unitModelGameObject.AddComponent<UnitModelBase>();
		unitModel.initialize();
		return unitModel;
	}

	// ------------------------------------------------------------------------------------ //

	private Animator animator;

	public void initialize () 
	{
		animator = gameObject.GetComponent<Animator>();
	}

	public void startAnimation (UnitModelAnimationType type)
	{
		animator.Play(type.ToString());
	}

	// ------------------------------------------------------------------------------------ //

}
