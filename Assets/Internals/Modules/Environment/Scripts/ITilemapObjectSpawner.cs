using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicLegionInfected.Environment
{
    public interface ITilemapObjectSpawner
    {
        void SpawnObject(Tilemap tilemap, Vector3Int tilePosition);
        void DestroyAllObjects();
    }
}
