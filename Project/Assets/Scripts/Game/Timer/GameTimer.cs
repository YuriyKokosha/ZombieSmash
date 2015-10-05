using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameTimer : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //

	public static GameTimer createNewInstance (GameObject gameObject) 
	{
		GameTimer gameTimer = gameObject.AddComponent<GameTimer>();
		gameTimer.initialize();
		return gameTimer;
	}

	// ------------------------------------------------------------------------------------ //

	private List<IGameTimerObserver> gameTimerObservers = new List<IGameTimerObserver> ();

	private float life;
	private float createUnitTime;

	private float createUnitDeltaTime = 2.0f;
	private float createLastUnitTime = 7.0f;
	private int gameTime = 60;

	private bool timeEnd = false;

	public void initialize ()
	{
		life = gameTime + 0.5f;
		createUnitTime = gameTime - createUnitDeltaTime;
	}

	// ------------------------------------------------------------------------------------ //

	public int getTime () {
		return gameTime;
	}

	public void addTime (int time) {
		life += time;
	}

	// ------------------------------------------------------------------------------------ //
	
	void Update ()
	{
		if (!TimeController.getPause())
		{
			life -= Time.deltaTime;

			if (life >= 0) 
			{
				// check game time 
				if (gameTime != (int) life)
				{
					gameTime = (int) life;

					foreach (IGameTimerObserver observer in gameTimerObservers.ToArray()) {
						observer.onTimeValueChanged(gameTime);
					}
				}

				// check create unit time
				if (life < createUnitTime && life > createLastUnitTime) 
				{
					createUnitTime = life - createUnitDeltaTime;

					foreach (IGameTimerObserver observer in gameTimerObservers.ToArray()) {
						observer.onCreateNewUnitTime();
					}
				}
			}
			else if (timeEnd == false) 
			{
				timeEnd = true;

				foreach (IGameTimerObserver observer in gameTimerObservers.ToArray()) {
					observer.onTimeEnd();
				}
			}
		}
	}

	// ------------------------------------------------------------------------------------ //
	
	public void registerObserver (IGameTimerObserver observer) {
		gameTimerObservers.Add(observer);
	}
	public void removeObserver (IGameTimerObserver observer) {
		gameTimerObservers.Remove(observer);
	}

	// ------------------------------------------------------------------------------------ //
}
