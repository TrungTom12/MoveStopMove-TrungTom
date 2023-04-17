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
            if (c is Bot)
            {
                if (c.GetComponent<Bot>().CurrentState is DieState)
                {
                    list.Add(c);
                }
            }
            else
            {
                if (c.GetComponent<Player>().MyState is PlayerState.Dead)
                {
                    list.Add(c);
                }
            }
        }

        foreach (Character c in list) //ktr có các phần tư roi xoa 
        {
            l_charac.Remove(c);
        }
        ISHAVEPLAYER = l_charac.Count > 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bot")
        {
            if (!l_charac.Contains(other.GetComponent<Character>()))
                l_charac.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bot")
        {
            
            if (l_charac.Contains(other.GetComponent<Character>()))
                l_charac.Remove(other.GetComponent<Character>());
        }
    }

    public bool IsAnyPlayer()
    {
        return l_charac.Count > 0;
    }

}
