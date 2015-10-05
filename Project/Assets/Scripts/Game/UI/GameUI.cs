using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //
	
	public static GameUI createNewInstance (GameObject gameObject) 
	{
		GameUI gameUI = gameObject.AddComponent<GameUI>();
		gameUI.initialize();
		return gameUI;
	}

	// ------------------------------------------------------------------------------------ //

	private Canvas gameCanvas;

	private Text timerText;
	private List<Image> healthImages = new List<Image> (); 
	private GameBombsUI bombsUI;

	private int healthIconsCount = 3;

	public void initialize ()
	{
		gameCanvas = CanvasBase.getSceneCanvas("GameCanvas");

		// initialize timer
		GameObject timerTextGameObject = gameCanvas.transform.FindChild("TimerText").gameObject;
		timerText = timerTextGameObject.GetComponent<Text>();

		// initialize health
		healthImages = new List<Image> ();
		for (int i=1; i<=healthIconsCount; i++) {
			GameObject healthImageGameObject = gameCanvas.transform.FindChild("HeartIcon"+i.ToString()).gameObject;
			healthImages.Add(healthImageGameObject.GetComponent<Image>());
		}

		// initialize bombs
		GameObject bombIconGameObject = gameCanvas.transform.FindChild("BombIcon").gameObject;
		bombsUI = GameBombsUI.createNewInstance(bombIconGameObject);
	}

	// ------------------------------------------------------------------------------------ //

	public void setTimerText (int time) 
	{
		timerText.text = TimePrint.timeToText(time, TimePrint.TimeFormat.MS);
	}

	public void setHealth (int health) 
	{
		for (int i=1; i<=healthIconsCount; i++) {
			if (health < i) {
				healthImages[i-1].enabled = false;
			}
			else {
				healthImages[i-1].enabled = true;
			}
		}
	}

	public GameBombsUI getGameBombsUI () 
	{
		return bombsUI;
	}

	// ------------------------------------------------------------------------------------ //

}
