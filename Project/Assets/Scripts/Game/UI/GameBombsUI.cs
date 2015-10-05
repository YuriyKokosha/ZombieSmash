using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameBombsUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	// ------------------------------------------------------------------------------------ //
	
	public static GameBombsUI createNewInstance (GameObject gameObject) 
	{
		GameBombsUI gameBombsUI = gameObject.AddComponent<GameBombsUI>();
		gameBombsUI.initialize();
		return gameBombsUI;
	}

	// ------------------------------------------------------------------------------------ //

	private List<IGameBombsUIObserver> gameBombsUIObservers = new List<IGameBombsUIObserver> ();

	private Vector3 startPosition;
	private Text bombsText;
	private bool interactionEnabled = true;

	public void initialize ()
	{
		GameObject bombsTextGameObject = transform.FindChild("BombText").gameObject;
		bombsText = bombsTextGameObject.GetComponent<Text>();
	}

	// ------------------------------------------------------------------------------------ //

	public void setBombs (int count) {
		bombsText.text = count.ToString();
	}

	public void setInteractionEnabled (bool enabled) {
		interactionEnabled = enabled;
	}

	// ------------------------------------------------------------------------------------ //

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (interactionEnabled) {
			startPosition = transform.position;
			bombsText.enabled = false;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (interactionEnabled) {
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if (interactionEnabled) 
		{
			foreach (IGameBombsUIObserver observer in gameBombsUIObservers.ToArray()) {
				observer.onBombEndDrag(transform.position);
			}
			
			transform.position = startPosition;
			bombsText.enabled = true;
		}
	}

	// ------------------------------------------------------------------------------------ //

	public void registerObserver (IGameBombsUIObserver observer) {
		gameBombsUIObservers.Add(observer);
	}
	public void removeObserver (IGameBombsUIObserver observer) {
		gameBombsUIObservers.Remove(observer);
	}

	// ------------------------------------------------------------------------------------ //
}


