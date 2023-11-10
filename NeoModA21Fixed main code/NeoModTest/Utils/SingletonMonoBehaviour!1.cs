namespace NeoModTest.Utils
{
    using System;
    using UnityEngine;

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T>
    {
        public static T Instance;
        public bool IsPersistant;

        protected virtual void Awake()
        {
            if (this.IsPersistant)
            {
                if (SingletonMonoBehaviour<T>.Instance == 0)
                {
                    SingletonMonoBehaviour<T>.Instance = (T) this;
                    SingletonMonoBehaviour<T>.Instance.singletonCreated();
                    UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
                }
                else
                {
                    UnityEngine.Object.Destroy(base.gameObject);
                }
            }
            else
            {
                SingletonMonoBehaviour<T>.Instance = (T) this;
                SingletonMonoBehaviour<T>.Instance.singletonCreated();
            }
            SingletonMonoBehaviour<T>.Instance.singletonAwake();
        }

        protected virtual void OnDestroy()
        {
            if (this == SingletonMonoBehaviour<T>.Instance)
            {
                this.singletonDestroy();
            }
        }

        protected virtual void singletonAwake()
        {
        }

        protected virtual void singletonCreated()
        {
        }

        protected virtual void singletonDestroy()
        {
        }
    }
}

