using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

using AYellowpaper;

using BasicLegionInfected.Core;
using BasicLegionInfected.Environment;
using System;

namespace BasicLegionInfected.Game
{
	public class LevelManager : MonoBehaviour
	{
		[Header("Component")]
		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _personSpawner;
		[field: SerializeField] public Tilemap Tilemap { get; private set; }
		[SerializeField] private RoomProceduralGenerator _roomProceduralGenerator;

		[Header("Configuration")]
		public int LevelWidth = 50;
		public int LevelHeight = 50;
		public int RoomMinSize = 5;
		public int RoomMaxSize = 10;
		public int RoomCount = 5;
		public int PersonInRoomCount = 2;

		// Event
		public AsyncEvent OnLoadEnvironmentAsyncEvent { get; private set; } = new();

		public async Task LoadLevel()
		{
			Clear();

			GenerateEnvironment();
			await OnLoadEnvironmentAsyncEvent.InvokeAsync();
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

		private void Clear()
		{
            _personSpawner.Value.DestroyAllObjects();
        }

		private void PlaceObjects()
		{
			PlacePerson();
		}

		private void PlacePerson()
		{
			foreach (RectInt room in _roomProceduralGenerator.Rooms)
			{
				Vector3Int spawnPosition = new((int)room.center.x, (int)room.center.y, 0);

				for(int i = 0; i < PersonInRoomCount; i++)
				{
					_personSpawner.Value.SpawnObject(Tilemap, spawnPosition);
				}
			}
		}
	}
}