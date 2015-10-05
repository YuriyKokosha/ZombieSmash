using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class UnitBase : MonoBehaviour, GameTouchController.IGameTouchEvent
{
	// ------------------------------------------------------------------------------------ //

	public const string UNIT_TAG = "Unit";

	// ------------------------------------------------------------------------------------ //

	private List<IUnitCollisionObserver> unitCollisionObservers = new List<IUnitCollisionObserver> ();
	private List<IUnitRunAwayObserver> unitRunAwayObservers = new List<IUnitRunAwayObserver> ();
	private List<IUnitDeadObserver> unitDeadObservers = new List<IUnitDeadObserver> ();

	private UnitStateBase state;
	private UnitModelBase model;
	private UnitType type;

	// ------------------------------------------------------------------------------------ //

	void Update () {
		if (!TimeController.getPause()) {
			updateUnit();
		}
	}

	void OnTriggerEnter(Collider other) {
		notifyUnitCollisionObservers(other.gameObject);
	}

	void OnParticleCollision(GameObject other) {
		notifyUnitCollisionObservers(other);
	}

	// ------------------------------------------------------------------------------------ //

	public abstract void initialize ();
	public abstract void createUnitModel ();
	public abstract void updateUnit ();

	public abstract void setStateDead ();
	public abstract void setStateRun ();

	// ------------------------------------------------------------------------------------ //

	public virtual void onTouchBegan (GameTouchController gameTouchController) {
		notifyUnitDeadObservers();
		setStateDead();
	}

	public virtual void destroyUnit () {
		Destroy(gameObject);
	}

	// ------------------------------------------------------------------------------------ //

	public UnitModelBase getUnitModel () {
		return model;
	}

	public void setUnitModel (UnitModelBase unitModel) {
		this.model = unitModel;
	}

	public UnitStateBase getUnitState () {
		return state;
	}

	public void setUnitState (UnitStateBase unitState) {
		if (state != null) {
			state.destroyState();
		}
		state = unitState;
		state.initialize(this);
	}

	public void updateState () {
		if (state != null) {
			state.updateState();
		}
	}

	public UnitType getUnitType () {
		return type;
	}
	
	public void setUnitType (UnitType unitType) {
		this.type = unitType;
	}

	// ------------------------------------------------------------------------------------ //
	
	public void addBoxCollider (Vector3 size, Vector3 center) {
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.isTrigger = true;
		boxCollider.size = size;
		boxCollider.center = center;
	}

	public void addCapsuleCollider (float radius, float height, Vector3 center) {
		CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
		capsuleCollider.isTrigger = true;
		capsuleCollider.radius = radius;
		capsuleCollider.height = height;
		capsuleCollider.center = center;
	}

	public void addRigidbody () {
		Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
		rigidbody.isKinematic = true;
		rigidbody.useGravity = false;
	}
	
	// ------------------------------------------------------------------------------------ //
	
	public void registerObserver (IUnitCollisionObserver observer) {
		unitCollisionObservers.Add(observer);
	}
	public void removeObserver (IUnitCollisionObserver observer) {
		unitCollisionObservers.Remove(observer);
	}

	public void notifyUnitCollisionObservers (GameObject other) {
		foreach (IUnitCollisionObserver observer in unitCollisionObservers.ToArray()) {
			if (observer != null) { 
				observer.onUnitCollision(other);
			}
		}
	}

	// ------------------------------------------------------------------------------------ //

	public void registerObserver (IUnitRunAwayObserver observer) {
		unitRunAwayObservers.Add(observer);
	}
	public void removeObserver (IUnitRunAwayObserver observer) {
		unitRunAwayObservers.Remove(observer);
	}

	public void notifyUnitRunAwayObservers () {
		foreach (IUnitRunAwayObserver observer in unitRunAwayObservers.ToArray()) {
			if (observer != null) { 
				observer.onUnitRunAway(this);
			}
		}
	}

	// ------------------------------------------------------------------------------------ //

	public void registerObserver (IUnitDeadObserver observer) {
		unitDeadObservers.Add(observer);
	}
	public void removeObserver (IUnitDeadObserver observer) {
		unitDeadObservers.Remove(observer);
	}

	public void notifyUnitDeadObservers () {
		foreach (IUnitDeadObserver observer in unitDeadObservers.ToArray()) {
			if (observer != null) { 
				observer.onUnitDead(this);
			}
		}
	}

	// ------------------------------------------------------------------------------------ //
}
