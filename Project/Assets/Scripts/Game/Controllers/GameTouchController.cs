using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTouchController : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //

	public interface IGameTouchEvent 
	{
		void onTouchBegan (GameTouchController gameTouchController);
	}

	// ------------------------------------------------------------------------------------ //
	
	public static GameTouchController createNewInstance (GameObject gameObject) 
	{
		GameTouchController gameTouchController = gameObject.AddComponent<GameTouchController>();
		gameTouchController.initialize();
		return gameTouchController;
	}

	// ------------------------------------------------------------------------------------ //

	private List<Camera> cameras = new List<Camera>();
	private Ray ray;
	private RaycastHit hit;

	public void initialize ()
	{

	}

	public void addCamera (Camera camera) {
		cameras.Add(camera);
	}

	public void removeCamera (Camera camera) {
		cameras.Remove(camera);
	}

	// ------------------------------------------------------------------------------------ //

	void Update () 
	{
		if (!TimeController.getPause())
		{
			if (Input.GetMouseButtonDown (0))
			{
				foreach (Camera camera in cameras)
				{
					ray = camera.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit, 1000)) {
						hit.collider.gameObject.SendMessage("onTouchBegan", this, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// ------------------------------------------------------------------------------------ //

}
