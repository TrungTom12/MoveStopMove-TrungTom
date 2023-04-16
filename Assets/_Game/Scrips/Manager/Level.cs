using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int Alive;
    public int NumBot;
    [SerializeField] private List<Transform> l_SpawnPos = new List<Transform>();

    public List<Transform> L_SpawnPos { get => l_SpawnPos; set => l_SpawnPos = value; }

    public void Win()
    {

    }
}
