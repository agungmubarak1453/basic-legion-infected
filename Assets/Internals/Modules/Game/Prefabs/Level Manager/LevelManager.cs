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

		public int LevelWidth = 50;
		public int LevelHeight = 50;
		public int RoomMinSize = 5;
		public int RoomMaxSize = 10;
		public int RoomCount = 5;

		public int PersonInRoomCount = 2;

		public void LoadLevel()
		{
			GenerateEnvironment();
			PlaceObjects();
		}

		private void GenerateEnvironment()
		{
			_roomProceduralGenerator.LevelWidth = LevelWidth;
			_roomProceduralGenerator.LevelHeight = LevelHeight;
			_roomProceduralGenerator.RoomMinSize = RoomMinSize;
			_roomProceduralGenerator.RoomMaxSize = RoomMaxSize;
			_roomProceduralGenerator.RoomCount = RoomCount;

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

				for(int i = 0; i < PersonInRoomCount; i++)
				{
					_personSpawner.Value.SpawnObject(_tilemap, spawnPosition);
				}
			}
		}
	}
}