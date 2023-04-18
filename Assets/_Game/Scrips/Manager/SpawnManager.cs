using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<Transform> SpawnBot(int numBot)
    {
        //Player
        List<Transform> transforms = new List<Transform>();
        Player player = PoolingPro.GetInstance().GetFromPool(CharacterType.Player.ToString(), LevelManager.GetInstance().CurrentLevel.L_SpawnPos[numBot].position).GetComponent<Player>();
        transforms.Add(player.transform);
        player.OnInit();
        GameManager.GetInstance().CurrentPlayer = player;

        //Bot
        for (int i = 0; i < numBot; i++)
        {
            if (i >= GameManager.GetInstance().L_SpawnBot.Count)
            {
                break;
            }
            GameObject go = PoolingPro.GetInstance().GetFromPool(CharacterType.Bot.ToString(), LevelManager.GetInstance().CurrentLevel.L_SpawnPos[i].position);
			//lay random weapon bot
			go.GetComponent<Bot>().ChangeEquipment(PoolingPro.GetInstance().weapons[Random.Range(0, PoolingPro.GetInstance().weapons.Count)]);
            transforms.Add(go.transform);
        }
        return transforms;

    }
}
