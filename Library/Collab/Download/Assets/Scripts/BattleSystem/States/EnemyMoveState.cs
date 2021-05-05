using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : BattleState
{
    private Stats stats;
    private List<Skill> damageSkills = new List<Skill>();
    private List<Skill> healSkills = new List<Skill>();
    int maxAPCost = 0;
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy State");
        statPanelController.ShowPlayer(player);
        statPanelController.ShowMob(monster);
        stats = turn.curActor.GetComponent<Stats>();
        foreach (Skill skill in turn.curActor.GetComponentsInChildren<Skill>())
        {
            if (skill.GetComponentInChildren<HealSkillEffect>() != null)
                healSkills.Add(skill);
            else
                damageSkills.Add(skill);
            if (skill.GetComponent<SkillAPCost>() != null)
                if (skill.GetComponent<SkillAPCost>().amount > maxAPCost)
                    maxAPCost = skill.GetComponent<SkillAPCost>().amount;
        }
        StartCoroutine(EnemyAttack());
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePlayer();
        statPanelController.HideMob();
    }
    IEnumerator EnemyAttack()
    {
        Debug.Log("Stats " + stats);
        if (stats[StatTypes.AP] < maxAPCost)
        {
            turn.skill = damageSkills[0];
        }
        else if (stats[StatTypes.HP] < stats[StatTypes.MHP] * .75 && healSkills.Count > 0)
        {
            turn.skill = healSkills[Random.Range(0, healSkills.Count)];
        }
        else if (damageSkills.Count > 1)
        {
            turn.skill = damageSkills[Random.Range(1, healSkills.Count)];
        }
        else
        {
            turn.skill = damageSkills[0];
        }
        Debug.Log("Chosen Skill: " + turn.skill);
        yield return new WaitForSeconds(1f);
        
        Manager.ChangeState<PerformActionState>();
    }
}
