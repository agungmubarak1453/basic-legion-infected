using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

using AYellowpaper;

using BasicLegionInfected.Environment;

namespace BasicLegionInfected.Game
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private GameObject _personPrefab;

		[SerializeField] private Tilemap _tilemap;
		[SerializeField] private RoomProceduralGenerator _roomProceduralGenerator;

		private List<GameObject> _objects = new();

		public void LoadLevel()
		{
			GenerateEnvironment();
			PlaceObjects();
		}

		private void GenerateEnvironment()
		{
			_roomProceduralGenerator.Generate();
		}

		private void PlaceObjects()
		{
			foreach (GameObject _object in _objects)
			{
				Destroy(_object);
			}

			_objects.Clear();

			PlacePerson();
		}

		private void PlacePerson()
		{
			foreach (RectInt room in _roomProceduralGenerator.Rooms)
			{
				Vector2 spawnPosition = room.center;
				GameObject person = GameObject.Instantiate(_personPrefab, spawnPosition, Quaternion.identity);

				_objects.Add(person);
			}

			Debug.Log("Mock PlacePerson");
		}
	}
}