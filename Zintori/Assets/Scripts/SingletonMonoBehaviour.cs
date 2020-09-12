using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning(t + "is nothing.");
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)
        {
            // 存在したらオブジェクトごと消し去る
            Destroy(gameObject);
            return;
        }
    }
}