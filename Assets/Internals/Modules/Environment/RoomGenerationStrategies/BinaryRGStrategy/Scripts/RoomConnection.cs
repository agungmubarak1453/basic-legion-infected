using UnityEngine;

namespace BasicLegionInfected.Environment.RoomGenerationStrategies
{
	public partial class BinaryRGStrategy
	{
		public class RoomConnection
		{
			public readonly Room RoomA;
			public readonly Room RoomB;

			public RoomConnection(Room roomA, Room roomB)
			{
				RoomA = roomA;
				RoomB = roomB;
			}

			public (Vector3Int pointA, Vector3Int pointB) GetIntersectionPoints()
			{
				RectInt rectA = RoomA.Rect;
				RectInt rectB = RoomB.Rect;

				int left = Mathf.Max(rectA.xMin, rectB.xMin);
				int right = Mathf.Min(rectA.xMax, rectB.xMax);
				int bottom = Mathf.Max(rectA.yMin, rectB.yMin);
				int top = Mathf.Min(rectA.yMax, rectB.yMax);
				RectInt intersectionRect = new RectInt(left, bottom, right - left, top - bottom);

				Vector3Int minPoint = new(intersectionRect.xMin, intersectionRect.yMin, 0);
				Vector3Int maxPoint = new(intersectionRect.xMax, intersectionRect.yMax, 0);

				return (minPoint, maxPoint);
			}

			public bool IsSameRoomConnection(RoomConnection roomConnection)
			{
				return IsConnectionFromThoseRooms(roomConnection, RoomA, RoomB);
			}

			public static bool IsConnectionFromThoseRooms(RoomConnection roomConnection, Room roomA, Room roomB)
			{
				return (roomConnection.RoomA == roomA && roomConnection.RoomB == roomB) ||
					   (roomConnection.RoomA == roomB && roomConnection.RoomB == roomA);
			}
		}
	}
}
