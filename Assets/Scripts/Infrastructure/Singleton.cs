using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        public static bool IsInitialized => instance != null;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("[Singleton] Attempt to instantiate a second instance of a singleton");
            }
            else
            {
                instance = (T) this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
