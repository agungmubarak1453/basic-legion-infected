using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Utility
{
    public static class Randomizer
    {
		public static void Shuffle<T>(T[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				int randomIndex = Random.Range(i, array.Length);
				T temp = array[i];
				array[i] = array[randomIndex];
				array[randomIndex] = temp;
			}
		}

		public static Vector3 GetRandomPointInOval(Vector2Int size)
		{
			float theta = Random.Range(0, 2 * Mathf.PI);
			float rad = Mathf.Sqrt(Random.Range(0, 1f));

			return new Vector3(size.x * rad * Mathf.Cos(theta), size.y * rad * Mathf.Sin(theta));
		}

		public static Vector3 GetRandomPointInRect(Vector2Int size)
		{
			float width = Random.Range(-size.x, size.x);
			float height = Random.Range(-size.y, size.y);
			return new Vector3(width, height, 0);
		}

		public static Vector3 GetRandomScale(int minS, int maxS)
		{
			int x = Random.Range(minS, maxS) * 2;
			int y = Random.Range(minS, maxS) * 2;

			return new Vector3(x, y, 1);
		}
	}
}
