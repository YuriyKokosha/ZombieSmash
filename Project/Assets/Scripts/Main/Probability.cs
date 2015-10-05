using UnityEngine;
using System.Collections;

public class Probability 
{
	public static bool getProbability (int percent) 
	{
		if (percent >= 1 && percent <= 100) 
		{
			if (Random.Range(1,101) <= percent) {
				return true;
			}
			else {
				return false;
			}
		}

		SLog.logError("ProbabilityBase get() error: percent == "+percent.ToString());
		return false;
	}
}
