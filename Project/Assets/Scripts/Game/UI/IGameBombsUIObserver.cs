using UnityEngine;
using System.Collections;

public interface IGameBombsUIObserver
{
	void onBombEndDrag (Vector3 touchPosition);
}