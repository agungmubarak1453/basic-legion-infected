using System.Collections.Generic;

using UnityEngine;

namespace BasicLegionInfected.Environment.RoomGenerationStrategies
{
    public partial class BinaryRGStrategy
	{
        public class Room
        {
            public readonly RectInt Rect;

            public readonly List<RoomConnection> Connections = new();

            public Room(RectInt rect)
            {
                Rect = rect;
            }

            public void AddConnection(Room otherRoom)
            {
                RoomConnection otherExistingConnection = otherRoom.GetConnectionToThis(this);

                //Debug.Log($"Existing connection from room {Rect}: {otherExistingConnection}");

				if (otherExistingConnection == null)
                {
                    RoomConnection newConnection = new(this, otherRoom);

                    Connections.Add(newConnection);
                    otherRoom.AddConnection(this);
                }
                else
                {
                    if(GetConnectionToThis(otherRoom) == otherExistingConnection)
                    {
                        return;
                    }

                    Connections.Add(otherExistingConnection);
				}
            }

            public RoomConnection GetConnectionToThis(Room room)
            {
				RoomConnection connection = Connections.Find(c => RoomConnection.IsConnectionFromThoseRooms(c, this, room));

                return connection;
			}
		}
    }
}
