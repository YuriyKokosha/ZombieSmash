using UnityEngine;
using System.Collections;

public class PopupFactory : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------ //

	public static PopupBase createPopup (GameObject popupGameObject, 
	                                     PopupType popupType) 
	{
		if (popupType == PopupType.LevelFailed) {
			return popupGameObject.AddComponent<PopupLevelFailed>();
		}
		else if (popupType == PopupType.LevelCompleted) {
			return popupGameObject.AddComponent<PopupLevelCompleted>();
		}

		SLog.logError("PopupFactory createNewInstance(): unknown type == " + popupType.ToString());
		return null;
	}

	// ------------------------------------------------------------------------------------ //
}
