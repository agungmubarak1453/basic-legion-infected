using UnityEngine;

namespace BasicLegionInfected.Utility
{
    public static class Rounder
    {
		public static int RoundToNearestMultiplier(float value, int multiplier)
		{
			return Mathf.FloorToInt(((value + multiplier - 1) / multiplier)) * multiplier;
		}
	}
}
