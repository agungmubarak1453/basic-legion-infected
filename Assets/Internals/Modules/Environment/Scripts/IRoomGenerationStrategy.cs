using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment
{
	public interface IRoomGenerationStrategy
	{
		void Execute(List<RectInt> rooms, Tilemap tilemap, ITilemapObjectSpawner wallTileSpawner, ITilemapObjectSpawner doorTileSpawner, int levelWidth, int levelHeight, int roomMinSize, int roomMaxSize, int roomCount);
	}
}
