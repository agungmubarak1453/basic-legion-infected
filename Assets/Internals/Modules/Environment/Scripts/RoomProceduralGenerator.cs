using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace BasicLegionInfected.Environment
{
    public class RoomProceduralGenerator : MonoBehaviour
    {
		[SerializeField] private Tilemap _tilemap;

		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _wallTileSpawner;
		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _doorTileSpawner;

		public InterfaceReference<IRoomGenerationStrategy> RoomGenerationStrategy; 

		// For precedural generation
		public int LevelWidth = 50;
		public int LevelHeight = 50;
		public int RoomMinSize = 5;
		public int RoomMaxSize = 10;
		public int RoomCount = 5;

		public List<RectInt> Rooms { get; private set; } = new();

		public RectInt[] Generate()
		{
			_wallTileSpawner.Value.DestroyAllObjects();
			_doorTileSpawner.Value.DestroyAllObjects();

			Rooms.Clear();

			RoomGenerationStrategy.Value.Execute(Rooms, _tilemap, _wallTileSpawner.Value, _doorTileSpawner.Value, LevelWidth, LevelHeight, RoomMinSize, RoomMaxSize, RoomCount);

			return Rooms.ToArray();
		}
	}
}
