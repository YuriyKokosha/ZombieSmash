using UnityEngine;
using System.Collections;

public class GameBomb : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //
	
	public static GameBomb createBomb (Vector3 position) 
	{
		GameObject bombGameObject = Instantiate (ResourcesBase.load("Prefabs/Bomb/Fire")) as GameObject;
		bombGameObject.name = "Bomb";

		bombGameObject.transform.position = position;

		GameBomb gameBomb = bombGameObject.AddComponent<GameBomb>();
		return gameBomb;
	}

	// ------------------------------------------------------------------------------------ //

	private float life = 0.5f;	// destroy bomb after 2.0 seconds

	void Start () 
	{
		particleEmitter.emit = false;
		particleEmitter.Emit();
	}

	void Update () 
	{
		life -= Time.deltaTime;
		if (life < 0) {
			Destroy(gameObject);
		}
	}

	// ------------------------------------------------------------------------------------ //
}
