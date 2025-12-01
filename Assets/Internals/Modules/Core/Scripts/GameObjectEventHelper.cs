using UnityEngine;

using UnityEngine.Events;

namespace BasicLegionInfected.Core
{
    public class GameObjectEventHelper : MonoBehaviour
    {
		public UnityEvent OnStart = new();
		public UnityEvent OnUpdate = new();
		public UnityEvent _OnDestroy = new();

		private void Start()
		{
			OnStart.Invoke();
		}

		private void Update()
		{
			OnUpdate.Invoke();
		}

		private void OnDestroy()
		{
			_OnDestroy.Invoke();
		}
	}
}
