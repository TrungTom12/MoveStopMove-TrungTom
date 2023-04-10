using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        GetInstance();
    }

    [SerializeField] private List<Transform> l_character = new List<Transform>();

    public List<Transform> L_character { get => l_character; set => l_character = value; }
}
