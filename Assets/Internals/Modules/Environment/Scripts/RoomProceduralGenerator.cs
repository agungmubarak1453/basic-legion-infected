using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

using AYellowpaper;

namespace BasicLegionInfected.Environment
{
    public class RoomProceduralGenerator : MonoBehaviour
    {
		[SerializeField] private Tilemap _tilemap;

		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _wallTileSpawner;
		[SerializeField] private InterfaceReference<ITilemapObjectSpawner> _doorTileSpawner;

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

			GenerateRooms();
			ConnectRooms();

			return Rooms.ToArray();
		}

		private void GenerateRooms()
		{
			for (int i = 0; i < RoomCount; i++)
			{
				int width = Random.Range(RoomMinSize, RoomMaxSize + 1);
				int height = Random.Range(RoomMinSize, RoomMaxSize + 1);

				int maxXDistance = (LevelWidth + width) / 2;
				int maxYDistance = (LevelHeight + height) / 2;

				int x = Random.Range(-maxXDistance, maxXDistance);
				int y = Random.Range(-maxYDistance, maxYDistance);

				RectInt newRoom = new(x, y, width, height);

				// Check overlap with existing rooms  
				bool overlaps = false;
				foreach (RectInt room in Rooms)
				{
					if (room.Overlaps(newRoom))
					{
						overlaps = true;
						break;
					}
				}

				if (!overlaps)
				{
					Rooms.Add(newRoom);
					DrawRoom(newRoom);
				}
			}
		}

		private void DrawRoom(RectInt room)
		{
			for (int x = room.xMin; x <= room.xMax; x++)
			{
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(x, room.yMin, 0));
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(x, room.yMax, 0));
			}

			for (int y = room.yMin; y <= room.yMax; y++)
			{
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(room.xMin, y, 0));
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(room.xMax, y, 0));
			}
		}

		private void ConnectRooms()
		{
			for (int i = 0; i < Rooms.Count - 1; i++)
			{
				RectInt roomStart = Rooms[i];
				RectInt roomEnd = Rooms[i + 1];

				CreateCorridor(roomStart, roomEnd);
			}
		}

		private Vector3Int GetNearestWallEdge(RectInt room, Vector3Int target)
		{
			Vector3Int bottomCenter = new Vector3Int((room.xMin + room.xMax) / 2, room.yMin, 0);
			Vector3Int topCenter = new Vector3Int((room.xMin + room.xMax) / 2, room.yMax, 0);
			Vector3Int leftCenter = new Vector3Int(room.xMin, (room.yMin + room.yMax) / 2, 0);
			Vector3Int rightCenter = new Vector3Int(room.xMax, (room.yMin + room.yMax) / 2, 0);

			Vector3Int[] centers = { bottomCenter, topCenter, leftCenter, rightCenter };

			Vector3Int closest = centers[0];
			float minDistance = Vector3.Distance(closest, target);

			foreach (var center in centers)
			{
				float dist = Vector3.Distance(center, target);
				if (dist < minDistance)
				{
					minDistance = dist;
					closest = center;
				}
			}
			return closest;
		}

		private void CreateCorridor(RectInt roomStart, RectInt roomEnd)
		{
			int corridorWidth = 3;
			int halfWidth = corridorWidth / 2;

			Vector3Int start = GetNearestWallEdge(roomStart, new Vector3Int((int)roomEnd.center.x, (int)roomEnd.center.y, 0));
			Vector3Int end = GetNearestWallEdge(roomEnd, new Vector3Int((int)roomStart.center.x, (int)roomStart.center.y, 0));

			int xMin = Mathf.Min(start.x, end.x) - halfWidth;
			int xMax = Mathf.Max(start.x, end.x) + halfWidth;
			int yMin = Mathf.Min(start.y, end.y) - halfWidth;
			int yMax = Mathf.Max(start.y, end.y) + halfWidth;

			// Clear entire corridor rectangle
			for (int x = xMin; x <= xMax; x++)
			{
				for (int y = yMin; y <= yMax; y++)
				{
					_tilemap.SetTile(new Vector3Int(x, y, 0), null);
				}
			}

			// Draw corridor floors (empty space) along L-shaped path with width
			if (Random.Range(0, 2) == 0)
			{
				// Horizontal then vertical
				for (int x = start.x; x <= end.x; x++)
				{
					for (int w = -halfWidth; w <= halfWidth; w++)
					{
						_tilemap.SetTile(new Vector3Int(x, start.y + w, 0), null);
					}
				}
				for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
				{
					for (int w = -halfWidth; w <= halfWidth; w++)
					{
						_tilemap.SetTile(new Vector3Int(end.x, y + w, 0), null);
					}
				}
			}
			else
			{
				// Vertical then horizontal
				for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
				{
					for (int w = -halfWidth; w <= halfWidth; w++)
					{
						_tilemap.SetTile(new Vector3Int(start.x + w, y, 0), null);
					}
				}
				for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++)
				{
					for (int w = -halfWidth; w <= halfWidth; w++)
					{
						_tilemap.SetTile(new Vector3Int(x, end.y + w, 0), null);
					}
				}
			}

			// Draw walls on corridor edges (outer border of corridor rectangle)
			for (int x = xMin; x <= xMax; x++)
			{
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(x, yMin, 0));
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(x, yMax, 0));
			}
			for (int y = yMin; y <= yMax; y++)
			{
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(xMin, y, 0));
				_wallTileSpawner.Value.SpawnObject(_tilemap, new(xMax, y, 0));
			}

			// Place door tiles at corridor entrances
			// First remove wall tile on those entrances
			// Door at corridor start offset by 1 tile toward corridor if wall on edge

			Vector3Int startDoorPos = start;
			if (start.x == roomStart.xMin) startDoorPos.x += 1;
			else if (start.x == roomStart.xMax) startDoorPos.x -= 1;
			else if (start.y == roomStart.yMin) startDoorPos.y += 1;
			else if (start.y == roomStart.yMax) startDoorPos.y -= 1;
			_tilemap.SetTile(startDoorPos, null);
			_doorTileSpawner.Value.SpawnObject(_tilemap, startDoorPos);

			Vector3Int endDoorPos = end;
			if (end.x == roomEnd.xMin) endDoorPos.x += 1;
			else if (end.x == roomEnd.xMax) endDoorPos.x -= 1;
			else if (end.y == roomEnd.yMin) endDoorPos.y += 1;
			else if (end.y == roomEnd.yMax) endDoorPos.y -= 1;
			_tilemap.SetTile(endDoorPos, null);
			_doorTileSpawner.Value.SpawnObject(_tilemap, endDoorPos);
		}
	}
}
