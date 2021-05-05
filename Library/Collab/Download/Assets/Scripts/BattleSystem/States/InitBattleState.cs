using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        player = FindObjectOfType<Player>();
        playerLoc = GameObject.FindGameObjectWithTag("PlayerLoc");
        monsterLoc = GameObject.FindGameObjectWithTag("MonsterLoc");
        player.transform.position = playerLoc.transform.position;
        player.transform.rotation = playerLoc.transform.rotation;
        player.battling = true;
        monster = Instantiate(Resources.Load(player.mobName), monsterLoc.transform.position, monsterLoc.transform.rotation) as Unit;
        monster = FindObjectOfType<Enemy>() as Unit;
        Debug.Log("Monster: " + monster);
        turn.curActor = monster;
        turn.target = player;
        Manager.units.Clear();
        Manager.units.Add(monster);
        Manager.units.Add(player);
        Manager.round = Manager.gameObject.AddComponent<TurnOrderController>().Round();
        yield return null;
        Stats mobStats = monster.GetComponent<Stats>();
        mobStats[StatTypes.EXP] = ExpHandler.ExperienceForLevel(player.mobLevel);
        Manager.round.MoveNext();
        Manager.ChangeState<ChooseActionState>();
    }
}
