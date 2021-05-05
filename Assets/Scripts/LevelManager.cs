using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Unit player;
    private Player hero;
    public List<GameObject> starterItems;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Player Player = (Player)Instantiate(player, gameObject.transform.position, Quaternion.identity);

            foreach (GameObject item in starterItems)
            {
                GameObject Item = Resources.Load(item.name) as GameObject;
                Player.inventory.Add(Item);
            }
        }
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (hero.battling == true)
        {
            hero.transform.rotation = BattleData.StartRot;
            if (hero.wonBattle)
                hero.transform.position = BattleData.StartLoc;
            else
                hero.transform.position = BattleData.StartLoc + hero.transform.forward * -2;
            hero.transform.rotation = BattleData.StartRot;
            hero.battling = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
