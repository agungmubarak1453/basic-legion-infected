using UnityEngine;

using BasicLegionInfected.Core;
using System;

using UnityEngine.Events;

namespace BasicLegionInfected.Game
{
	#nullable enable
	public class GameManager : ASingleton<GameManager>
	{
		private Session? _currentSession;
		[SerializeField] private SessionManager _sessionManager;

		public UnityEvent OnGameReady = new();

		private int _currentLevel;

		private void Start()
		{
			OnGameReady.Invoke();
		}

		private void OnSessionClose()
		{
			LevelUp();
		}

		public void PlayGame()
		{
			_currentSession = _sessionManager.CreateGameSession();
			_currentSession.OnClose.AddListener(OnSessionClose);

			_currentLevel = 1;
			ConfigureSessionToLevel(_currentSession, _currentLevel);

			_currentSession.Start();
		}

		public void LevelUp()
		{
			_currentLevel++;

			_currentSession = _sessionManager.CreateGameSession();
			_currentSession.OnClose.AddListener(OnSessionClose);

			ConfigureSessionToLevel(_currentSession, _currentLevel);

			_currentSession.Start();
		}

		public void ExitGame()
		{
			_currentSession?.Close();
		}

		private void ConfigureSessionToLevel(Session session, int level)
		{
			int roomCount = Mathf.FloorToInt(1f + Mathf.Log(level, 1.3f) / 4f);
			//Debug.Log($"1f + Mathf.Log(level, 1.1f) / 4f): {1f + Mathf.Log(level, 1.1f) / 4f}");
			int personRoomCount = 2;
			int infectedCount = Mathf.CeilToInt(roomCount / 2f);
			//Debug.Log($"roomCount / 2: {roomCount / 2}");

			Debug.Log($"roomCount: {roomCount} personRoomCount:{personRoomCount} infectedCount:{infectedCount}");

			session.RoomCount = roomCount;
			session.PersonInRoomCount = personRoomCount;
			session.StartingInfectedCount = infectedCount;
		}
	}
}
