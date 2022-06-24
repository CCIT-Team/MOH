using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public bool dontDestroyOnLoad;

    private static volatile T _instance;
    private static object _syncRoot = new System.Object();

    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    public static bool isInitialized
    {
        get
        {
            return _instance != null;
        }
    }

    public static void Initialize()
    {
        if (_instance != null)
        {
            var instances = GameObject.FindObjectsOfType<T>();
            for (int i = 0; i < instances.Length; i++)
            {
                if (_instance.gameObject != instances[i].gameObject)
                {
                    Destroy(instances[i].gameObject);
                }

            }
            return;
        }
        lock (_syncRoot)
        {
            _instance = GameObject.FindObjectOfType<T>();
          

            if (_instance == null)
            {
                var go = new GameObject(typeof(T).FullName);
                _instance = go.AddComponent<T>();
            }
        }
    }

    protected virtual void Awake()
    {
        Initialize();
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        OnAwake();
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    protected virtual void OnAwake() { }
}
