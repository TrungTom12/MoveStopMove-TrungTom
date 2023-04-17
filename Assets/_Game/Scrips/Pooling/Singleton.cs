using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<T>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject();

                instance = gameObject.AddComponent<T>();
            }
            DontDestroyOnLoad(instance.gameObject);
        }
        return instance;

    }
}
