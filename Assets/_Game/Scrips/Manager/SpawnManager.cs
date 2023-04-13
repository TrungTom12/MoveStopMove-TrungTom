using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<Transform> SpawnBot(int numBot)
    {
        List<Transform> transforms = new List<Transform>();
        transforms.Add(PoolingPro.GetInstance().GetFromPool("Player", GameManager.GetInstance().L_SpawnBot[numBot].position).transform);

        for (int i = 0; i < numBot; i++)
        {
            if (i >= GameManager.GetInstance().L_SpawnBot.Count)
            {
                break;
            }

            GameObject go = PoolingPro.GetInstance().GetFromPool("Bot", GameManager.GetInstance().L_SpawnBot[i].position);
            transforms.Add(go.transform);
        }

        return transforms;

    }
}
