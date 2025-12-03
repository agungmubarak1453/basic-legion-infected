using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

using AYellowpaper;

using BasicLegionInfected.Environment;

namespace BasicLegionInfected.Game
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _personSpawner;

		[SerializeField] private Tilemap _tilemap;
		[SerializeField] private RoomProceduralGenerator _roomProceduralGenerator;

		public void LoadLevel()
		{
			GenerateEnvironment();
			PlaceObjects();
		}

		private void GenerateEnvironment()
		{
			_roomProceduralGenerator.Generate();
		}

		private void PlaceObjects()
		{
			PlacePerson();
		}

		private void PlacePerson()
		{
			_personSpawner.Value.DestroyAllObjects();

			foreach (RectInt room in _roomProceduralGenerator.Rooms)
			{
				Vector3Int spawnPosition = new((int)room.center.x, (int)room.center.y, 0);

				_personSpawner.Value.SpawnObject(_tilemap, spawnPosition);
			}

			Debug.Log("Mock PlacePerson");
		}
	}
}