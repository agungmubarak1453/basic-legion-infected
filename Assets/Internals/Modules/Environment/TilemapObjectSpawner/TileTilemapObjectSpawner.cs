using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment.TilemapObjectSpawners
{
	public class TileTilemapObjectSpawner : MonoBehaviour, ITilemapObjectSpawner
	{
		public Tile Tile;

		private List<(Tilemap, Vector3Int)> _tilePositions = new();

		public void SpawnObject(Tilemap tilemap, Vector3Int tilePosition)
		{
			tilemap.SetTile(tilePosition, Tile);

			_tilePositions.Add((tilemap, tilePosition));
		}

		public void DestroyAllObjects()
		{
			foreach ((Tilemap, Vector3Int) tilePosition in _tilePositions)
			{
				tilePosition.Item1.SetTile(tilePosition.Item2, null);
			}

			_tilePositions.Clear();
		}
	}
}
