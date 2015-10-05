using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour, IUnitRunAwayObserver, IUnitDeadObserver, IGameTimerObserver, IGameBombsUIObserver
{
	// ------------------------------------------------------------------------------------ //

	public static GameController createNewInstance (GameObject gameObject) 
	{
		GameController gameController = gameObject.AddComponent<GameController>();
		gameController.initialize();
		return gameController;
	}

	// ------------------------------------------------------------------------------------ //

	private GameTouchController gameTouchController;
	private GameCamera gameCamera;
	private GameTimer gameTimer;
	private GameUI gameUI;
	private GameBombsUI gameBombsUI;

	private int healthCount = 3;
	private int bombsCount = 3;

	public void initialize ()
	{
		gameCamera = GameCamera.createNewInstance();
		gameCamera.setPosition(new Vector3 (0,10.0f,0));
		gameCamera.setRotation(Quaternion.Euler(90.0f,0,0));

		gameTouchController = GameTouchController.createNewInstance(gameObject);
		gameTouchController.addCamera(gameCamera.getCamera());

		gameTimer = GameTimer.createNewInstance(gameObject);
		gameTimer.registerObserver((IGameTimerObserver) this);

		gameUI = GameUI.createNewInstance(gameObject);
		gameUI.setTimerText(gameTimer.getTime());
		gameUI.setHealth(healthCount);

		gameBombsUI = gameUI.getGameBombsUI();
		gameBombsUI.setBombs(bombsCount);
		gameBombsUI.registerObserver((IGameBombsUIObserver) this);
	}

	// ------------------------------------------------------------------------------------ //

	public void onTimeValueChanged (int time)
	{
		gameUI.setTimerText(time);
	}

	public void onTimeEnd ()
	{
		TimeController.setPause(true);
		SceneBase.getCurrentSceneClass().getPopupManager().showPopup(PopupType.LevelCompleted, null);
	}

	public void onCreateNewUnitTime ()
	{
		UnitType unitType = UnitFactory.getRandomUnitType();
		UnitBase unit = UnitFactory.createNewUnit(unitType);
		unit.gameObject.transform.position = new Vector3 ((Random.value-0.5f)*4.0f,0,7.0f);
		unit.gameObject.transform.rotation = Quaternion.Euler(0,180.0f,0);
		
		if (unitType != UnitType.ManDrunk) {
			unit.registerObserver((IUnitRunAwayObserver) this);
		}
		unit.registerObserver((IUnitDeadObserver) this);
		unit.setStateRun();
	}

	// ------------------------------------------------------------------------------------ //

	public void onBombEndDrag (Vector3 touchPosition)
	{
		bombsCount--;
		gameBombsUI.setBombs(bombsCount);

		if (bombsCount == 0) {
			gameBombsUI.setInteractionEnabled(false);
		}

		Ray ray;
		RaycastHit hit;

		ray = gameCamera.getCamera().ScreenPointToRay(touchPosition);
		if (Physics.Raycast(ray, out hit, 1000)) 
		{
			Vector3 bombPosition = hit.point;
			bombPosition.y = 1.0f;

			GameBomb.createBomb(bombPosition);
		}
	}

	// ------------------------------------------------------------------------------------ //

	public void onUnitRunAway (UnitBase unit) 
	{
		healthCount--;
		gameUI.setHealth(healthCount);

		if (healthCount == 0) {
			onLevelFailed();
		}
	}

	public void onUnitDead (UnitBase unit) 
	{
		if (unit.getUnitType() == UnitType.ManDrunk) 
		{
			onLevelFailed();
		}
	}

	private void onLevelFailed () 
	{
		TimeController.setPause(true);
		SceneBase.getCurrentSceneClass().getPopupManager().showPopup(PopupType.LevelFailed, null);
	}

	// ------------------------------------------------------------------------------------ //
}
