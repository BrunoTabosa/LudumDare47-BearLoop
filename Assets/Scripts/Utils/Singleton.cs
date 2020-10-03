using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearLoopGame.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;


        [Header("Singleton")]
        [SerializeField]
        protected bool _dontDestroyOnLoad = true;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instanceInScene = FindObjectOfType<T>();
                    if (instanceInScene == null)
                    {
                        _instance = new GameObject().AddComponent<T>();
                    }
                    else
                    {
                        _instance = instanceInScene;
                    }

                    var singleton = _instance as Singleton<T>;
                    _instance.gameObject.name = "_" + typeof(T).ToString();
                    if (singleton._dontDestroyOnLoad)
                    {
                        DontDestroyOnLoad(_instance);
                    }

                }

                return _instance;
            }

        }
        protected virtual void InitSingleton()
        {
            if (_instance != null)
            {
                //Debug.LogWarning("Trying to create a instance of singleton that already exists" + _instance.name, this);
                return;
            }

            _instance = Instance;
        }        
        public void DestroySingleton()
        {
            if (_instance != null)
            {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

    }
}
