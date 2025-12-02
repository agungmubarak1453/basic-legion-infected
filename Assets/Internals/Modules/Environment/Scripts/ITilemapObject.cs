using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment
{
    public interface ITilemapObject
    {
        Vector3Int GetTilemapPosition(Tilemap tilemap);
        Vector3Int GetWorldPosition(Tilemap tilemap);
        void SetPosition(Tilemap tilemap, Vector3Int tilePosition);
    }
}
