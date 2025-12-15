using UnityEngine;

namespace BasicLegionInfected.Utility
{
    public static class Rounder
    {
		public static int RoundToNearestMultiplier(float value, int multiplier)
		{
			return Mathf.FloorToInt(((value + multiplier - 1) / multiplier)) * multiplier;
		}

		public static bool IsNearFloat(float value, float near, float deviation)
		{
			return value >= near - deviation && value <= near + deviation; 
		}

        public static bool IsNearVector3(Vector3 value, Vector3 near)
        {
			return IsNearVector3(value, near, 0.01f);
        }

        public static bool IsNearVector3(Vector3 value, Vector3 near, float deviation)
		{
			return Mathf.Abs(value.magnitude - near.magnitude) < deviation;
		}
	}
}
