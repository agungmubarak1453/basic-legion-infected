using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment.RoomGenerationStrategies
{
	public partial class BinaryRGStrategy : MonoBehaviour, IRoomGenerationStrategy
	{
		public void Execute(List<RectInt> rooms, Tilemap tilemap, ITilemapObjectSpawner wallTileSpawner, ITilemapObjectSpawner doorTileSpawner, int levelWidth, int levelHeight, int roomMinSize, int roomMaxSize, int roomCount)
		{
			int size = roomMinSize * roomCount;

			Vector2Int initialSize = new(size, size);
			Vector2Int initialPosition = new(- (initialSize.x / 2), - (initialSize.y / 2));

			Room initialRoom = new (new RectInt(initialPosition, initialSize));

			Room[] createdRooms = SplitRoom(initialRoom, roomMinSize, roomCount);

			List<Vector3Int> willBeRemovedTilePosition = new();

			foreach (Room room in createdRooms)
			{
				DrawSquareWalls(tilemap, wallTileSpawner, room.Rect);

				// For set up room connections
				foreach (Room adjacentRoom in GetAdjacentRooms(room, createdRooms))
				{
					bool hasExistingConnection = true;

					if (adjacentRoom.GetConnectionToThis(room) == null)
					{
						// There is no existing connection between those rooms

						hasExistingConnection = false;
					}

					room.AddConnection(adjacentRoom);

					if (!hasExistingConnection)
					{
						RoomConnection connection = room.GetConnectionToThis(adjacentRoom);

						var intersectionPoints = connection.GetIntersectionPoints();

						Vector3Int midPositionFromConnection = new(
							(intersectionPoints.pointA.x + intersectionPoints.pointB.x) / 2,
							(intersectionPoints.pointA.y + intersectionPoints.pointB.y) / 2,
							intersectionPoints.pointA.z
						);

						willBeRemovedTilePosition.Add(midPositionFromConnection); // Do this because there is double wall generation from each room

						doorTileSpawner.SpawnObject(tilemap, midPositionFromConnection);
					}
				}

				rooms.Add(room.Rect);
			}

			foreach (Vector3Int tilePosition in willBeRemovedTilePosition)
			{
				tilemap.SetTile(tilePosition, null); // Set wall tile null
			}

			Debug.Log($"BinaryRGStrategy created {rooms.Count} rooms.");
		}

		private void DrawSquareWalls(Tilemap tilemap, ITilemapObjectSpawner wallTileSpawner, RectInt rect)
		{
			// Spawn walls
			for (int x = rect.xMin; x < rect.xMax; x++)
			{
				for (int y = rect.yMin; y < rect.yMax; y++)
				{
					if (x > rect.xMin && x < rect.xMax && y > rect.yMin && y < rect.yMax)
					{
						continue;
					}

					Vector3Int tilePosition = new(x, y, 0);
					wallTileSpawner.SpawnObject(tilemap, tilePosition);
				}
			}

			// Spawn walls
			for (int x = rect.xMax; x > rect.xMin; x--)
			{
				for (int y = rect.yMax; y > rect.yMin; y--)
				{
					if (x > rect.xMin && x < rect.xMax && y > rect.yMin && y < rect.yMax)
					{
						continue;
					}

					Vector3Int tilePosition = new(x, y, 0);
					wallTileSpawner.SpawnObject(tilemap, tilePosition);
				}
			}
		}

		private Room[] SplitRoom(Room room, int roomMinSize, int neededRoomCount)
		{
			Debug.Log($"Needed room count {neededRoomCount}");

			if (neededRoomCount == 1)
			{
				return new Room[] { room };
			}

			bool isHorizontalSplit = Random.value > 0.5f;

			if (isHorizontalSplit && room.Rect.width < roomMinSize * 2) isHorizontalSplit = false;

			int width = isHorizontalSplit ? Random.Range(roomMinSize, room.Rect.width - roomMinSize) : room.Rect.width;
			int height = isHorizontalSplit ? room.Rect.height : Random.Range(roomMinSize, room.Rect.height - roomMinSize);

			Vector2Int splitPosition = room.Rect.position;
			Vector2Int splitSize = new(width, height);

			Vector2Int splitSize2 = room.Rect.size - splitSize;
			if (isHorizontalSplit)
			{
				splitSize2.y = splitSize.y;
			}
			else
			{
				splitSize2.x = splitSize.x;
			}

			Vector2Int splitPosition2 = splitPosition + splitSize;
			if (isHorizontalSplit)
			{
				splitPosition2.y = splitPosition.y;
			}
			else
			{
				splitPosition2.x = splitPosition.x;
			}

			Room room1 = new(new RectInt(splitPosition, splitSize));
			Room room2 = new(new RectInt(splitPosition2, splitSize2));

			Debug.Log($"Split: {room1.Rect.size} & {room2.Rect.size}");

			if (
				neededRoomCount > 1
			) {
				bool canRoom1Splits = room1.Rect.width >= 2 * roomMinSize || room1.Rect.height > 2 * roomMinSize;
				bool canRoom2Splits = room2.Rect.width >= 2 * roomMinSize || room2.Rect.height > 2 * roomMinSize;

				Room[] splitRooms1;
				Room[] splitRooms2;

				if (canRoom1Splits)
				{
					splitRooms1 = SplitRoom(room1, roomMinSize, Mathf.CeilToInt(neededRoomCount / 2));
				}
				else
				{
					splitRooms1 = new Room[] { room1 };
				}

				if (canRoom2Splits)
				{
					splitRooms2 = SplitRoom(room2, roomMinSize, neededRoomCount - splitRooms1.Length);
				}
				else
				{
					splitRooms2 = new Room[] { room2 };
				}

				Debug.Log($"Split room to {splitRooms1.Length} and {splitRooms2.Length} rooms");

				return splitRooms1.Concat(splitRooms2).ToArray();
			}
			else // This will handle for case needed room count < 1
			{
				return new Room[0];
			}
		}

		private Room[] GetAdjacentRooms(Room room, Room[] allRooms)
		{
			List<Room> adjacentRooms = new();

			foreach (Room otherRoom in allRooms)
			{
				if (room == otherRoom)
				{
					continue;
				}

				RectInt detectionRect = new(
					room.Rect.xMin - 1,
					room.Rect.yMin - 1,
					room.Rect.width + 2,
					room.Rect.height + 2
				);

				if (detectionRect.Overlaps(otherRoom.Rect))
				{
					adjacentRooms.Add(otherRoom);
				}
			}

			return adjacentRooms.ToArray();
		}
	}
}
