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
        statPanelController.ShowPlayer(player);
        statPanelController.ShowMob(monster);
        stats = turn.curActor.GetComponent<Stats>();
        foreach (Skill skill in turn.curActor.GetComponentsInChildren<Skill>())
        {
            if (skill.GetComponentInChildren<DamageSkillEffect>() != null)
                damageSkills.Add(skill);
            else if (skill.GetComponentInChildren<HealSkillEffect>() != null)
                healSkills.Add(skill);
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
            turn.skill = damageSkills[Random.Range(1, damageSkills.Count)];
        }
        else
        {
            turn.skill = damageSkills[0];
        }
        yield return new WaitForSeconds(1f);
        
        Manager.ChangeState<PerformActionState>();
    }
}
