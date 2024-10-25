using UnityEngine;

namespace Outsiders.Utility
{
	public class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindFirstObjectByType<T>();

					if (_instance == null)
					{
						GameObject singletonObject = new GameObject();
						_instance = singletonObject.AddComponent<T>();
						singletonObject.name = typeof(T).ToString() + " (Singleton)";
					}

					DontDestroyOnLoad(_instance.gameObject);
				}

				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this)
			{
				Destroy(gameObject);
			}
		}
	}
}
