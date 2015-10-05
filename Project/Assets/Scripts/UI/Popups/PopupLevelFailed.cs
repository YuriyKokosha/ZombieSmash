using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupLevelFailed : PopupBase 
{
	// ------------------------------------------------------------------------------------ //
	
	public override void initialize (PopupType type, int popupNumber, PopupParamsBase popupParams)
	{
		base.initialize(type, popupNumber, popupParams);

		addDelegateToButton("ButtonRetry", delegate() { SceneBase.getCurrentSceneClass().reloadScene(); });
	}

	// ------------------------------------------------------------------------------------ //
}
