using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformActionState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPlayer(player);
        statPanelController.ShowMob(monster);
        StartCoroutine(Perform());
    }
    IEnumerator Perform()
    {
        if(turn.curActor.GetComponent<Player>() != null)
        {
            Debug.Log("Anim player");
            turn.curActor.GetComponent<Player>().AttackAnim();
        }
        else
        {
            turn.curActor.GetComponent<Enemy>().AttackAnim();
        }
        yield return new WaitForSeconds(1.35f);
        ApplySkill();
        turn.hasUnitActed = true;
        Manager.ChangeState<EndTurnState>();
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePlayer();
        statPanelController.HideMob();

    }
    void ApplySkill()
    {
        Debug.Log("performed action");
        turn.skill.Perform(turn.target);
    }
}
