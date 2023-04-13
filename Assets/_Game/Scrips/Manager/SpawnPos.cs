using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    bool IsHaveCharacter = false;

    public bool IsAnyPlayer()
    {
        return IsHaveCharacter;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bot")
        {
            IsHaveCharacter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bot")
        {
            IsHaveCharacter=false;
        }
    }


}
