using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment.TilemapObjectSpawners
{
	public class GameObjectTilemapObject : MonoBehaviour, ITilemapObjectSpawner
	{
		public GameObject Prefab;

		private List<GameObject> _gameObjects = new();

		public void SpawnObject(Tilemap tilemap, Vector3Int tilePosition)
		{
			Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);

			GameObject spawnedGameObject = GameObject.Instantiate(
				Prefab, worldPosition, Quaternion.identity, transform
			);

			_gameObjects.Add(spawnedGameObject);
		}

		public void DestroyAllObjects()
		{
			foreach(GameObject gameObject in _gameObjects)
			{
				GameObject.Destroy(gameObject);
			}

			_gameObjects.Clear();
		}
	}
}
