using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState : BattleState
{
    private Stats heroStats;
    private Stats mobStats;
    Unit hero;
    Unit mob;
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPlayer(player);
        statPanelController.ShowMob(monster);
        hero = (turn.curActor.GetComponent<Player>() != null) ? turn.curActor : turn.target;
        mob = (turn.curActor.GetComponent<Player>() != null) ? turn.target : turn.curActor;
        heroStats = hero.GetComponent<Stats>();
        mobStats = mob.GetComponent<Stats>();
        StartCoroutine(CheckWin());
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePlayer();
        statPanelController.HideMob();
    }
    IEnumerator CheckWin()
    {
        yield return null;
        if (heroStats[StatTypes.HP] <= 0)
        {
            heroStats[StatTypes.HP] = Mathf.FloorToInt(heroStats[StatTypes.MHP] * 0.85f);
            Manager.ChangeState<BattleEndState>();
        }
        else if (mobStats[StatTypes.HP] <= 0)
        {
            ExperienceManager.AwardExperience(ExpHandler.ExperienceForLevel(mobStats[StatTypes.LVL]), hero);
            hero.GetComponent<Player>().wonBattle = true;
            Manager.ChangeState<BattleEndState>();
        }
        else
        {
            if (turn.hasUnitActed)
                Manager.round.MoveNext();
            if (turn.curActor.GetComponent<Enemy>() != null)
            {
                Debug.Log("Enemy turn");
                Manager.ChangeState<EnemyMoveState>();
            }
            Manager.ChangeState<ChooseActionState>();
        }
    }
}
