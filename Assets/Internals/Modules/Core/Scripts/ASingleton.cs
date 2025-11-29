using UnityEngine;

namespace BasicLegionInfected.Core
{
	public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
					{
						var obj = new GameObject($"Singleton - {typeof(T).Name}");
						_instance = obj.AddComponent<T>();
						DontDestroyOnLoad(obj);
					}
				}
				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
	}
}