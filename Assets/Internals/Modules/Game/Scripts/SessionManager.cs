using UnityEngine;

namespace BasicLegionInfected.Game
{
    public class SessionManager : MonoBehaviour
    {
        [SerializeField] EffectData _infectedEffectData;

        private Session _currentSession;

		private void Update()
		{
			_currentSession?.OnUpdate();
		}

		private void FixedUpdate()
		{
			_currentSession?.OnFixedUpdate();
		}

		public void OnSessionClose()
        {
			Debug.Log("SessionManager session closed.");
		}

        public Session CreateGameSession()
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            _currentSession = new(levelManager, _infectedEffectData, 2);
			_currentSession.OnClose.AddListener(OnSessionClose);

            _currentSession.OnStart();

			return _currentSession;
        }
    }
}
