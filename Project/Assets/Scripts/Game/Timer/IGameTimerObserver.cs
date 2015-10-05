﻿using UnityEngine;
using System.Collections;

public interface IGameTimerObserver 
{
	void onTimeValueChanged(int time);
	void onTimeEnd ();
	void onCreateNewUnitTime ();
}
