using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T mInstance;

    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<T>();
                if (mInstance == null)
                {
                    GameObject go = new GameObject();
                    mInstance = go.AddComponent<T>();
                }
            }
            return mInstance;
        }
    }
}
