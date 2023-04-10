using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T Instance;
    public static T GetInstance()
    {
        if (Instance == null)
        {
            Instance = GameObject.FindObjectOfType<T>();
            if (Instance == null)
            {
                GameObject gameObject = new GameObject();

                Instance = gameObject.AddComponent<T>();
            }
            DontDestroyOnLoad(Instance.gameObject);
        }
        return Instance;

    }
}
