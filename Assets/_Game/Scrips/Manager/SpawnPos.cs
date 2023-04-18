using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    public List<Character> l_charac = new List<Character>();
    public bool ISHAVEPLAYER = false;
   
    private void Update() //tạo ktr các phan tu theo dk và đung thì thêm vào list 
    {
        List<Character> list = new List<Character>();
        foreach (Character c in l_charac)
        {
            if (c.IsDead)
            {
                list.Add(c);
            }
        }

        foreach (Character c in list)
        {
            l_charac.Remove(c);
        }
        ISHAVEPLAYER = l_charac.Count > 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constan.TAG_PLAYER) || other.CompareTag(Constan.TAG_BOT))
        {
            if (!l_charac.Contains(Cache.GetCharacter(other)))
                l_charac.Add(Cache.GetCharacter(other));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constan.TAG_PLAYER) || other.CompareTag(Constan.TAG_BOT))
        {
            if (l_charac.Contains(Cache.GetCharacter(other)))
                l_charac.Remove(Cache.GetCharacter(other));
        }
    }

    public bool IsAnyPlayer()
    {
        return l_charac.Count > 0;
    }

}
