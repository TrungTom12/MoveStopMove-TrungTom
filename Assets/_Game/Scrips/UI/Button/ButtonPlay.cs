using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    [SerializeField] private string Level1 = "Level1";
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
