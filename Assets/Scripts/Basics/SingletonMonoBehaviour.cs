using System;

using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance;
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                Type t = typeof(T);
                s_instance = (T)FindAnyObjectByType(t);

                if (s_instance == null)
                {
                    Debug.Log($"No GameObject attaches {t}.");
                }
            }

            return s_instance;
        }
    }

    virtual protected void Awake() => CheckInstance();

    // Checks if the instance is attached to another GameObject.
    // If so, it destroys the current instance.
    protected bool CheckInstance()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
            return true;
        }
        else if (s_instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
}
