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
			Vector2Int initialPosition = new(- (levelWidth / 2), - (levelHeight / 2));
			Vector2Int initialSize = new(levelWidth, levelHeight);

			Room initialRoom = new (new RectInt(initialPosition, initialSize));

			Room[] createdRooms = SplitRoom(initialRoom, roomMinSize, roomMaxSize, roomCount);

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

		private Room[] SplitRoom(Room room, int roomMinSize, int roomMaxSize, int neededRoomCount)
		{
			if (neededRoomCount == 1)
			{
				return new Room[] { room };
			}

			bool isHorizontalSplit = Random.value > 0.5f;

			int width = isHorizontalSplit ? Random.Range(roomMinSize, room.Rect.width) : room.Rect.width;
			int height = isHorizontalSplit ? room.Rect.height : Random.Range(roomMinSize, room.Rect.height);

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

			if (splitSize2.x < roomMinSize || splitSize2.y < roomMinSize)
			{
				return new Room[] { room };
			}

			Room room1 = new(new RectInt(splitPosition, splitSize));
			Room room2 = new(new RectInt(splitPosition2, splitSize2));

			Debug.Log($"Split: {room1.Rect.size} & {room2.Rect.size}");
			Debug.Log($"newNeededRoomCount: {neededRoomCount}");

			if (
				neededRoomCount > 1
			) {
				return SplitRoom(room1, roomMinSize, Mathf.Min(room1.Rect.width, room1.Rect.height), neededRoomCount / 2).Concat(
					SplitRoom(room2, roomMinSize, Mathf.Min(room2.Rect.width, room2.Rect.height), neededRoomCount / 2)
				).ToArray();
			}
			else
			{
				return new Room[] { room1, room2 };
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
