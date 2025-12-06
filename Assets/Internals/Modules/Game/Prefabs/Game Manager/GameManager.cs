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

		private void Start()
		{
			OnGameReady.Invoke();
		}

		private void OnSessionClose()
		{
			PlayGame();
		}

		public void PlayGame()
		{
			_currentSession = _sessionManager.CreateGameSession();
			_currentSession.OnClose.AddListener(OnSessionClose);
		}

		public void ExitGame()
		{
			_currentSession?.Close();
		}
	}
}
