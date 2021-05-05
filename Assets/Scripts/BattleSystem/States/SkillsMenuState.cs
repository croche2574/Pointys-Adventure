using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuState : BaseActionMenuState
{
    public static int category;
    SkillCatalog catalog;
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPlayer(player);
        statPanelController.ShowMob(monster);
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePlayer();
        statPanelController.HideMob();
    }
    protected override void LoadMenu()
    {
        catalog = turn.curActor.GetComponentInChildren<SkillCatalog>();
        GameObject container = catalog.GetCategory(category);
        menuTitle = container.name;
        int count = catalog.SkillCount(container);
        if (menuOptions == null)
            menuOptions = new List<string>(count);
        else
            menuOptions.Clear();
        bool[] locks = new bool[count];
        for (int i = 0; i < count; ++i)
        {
            Skill skill = catalog.GetSkill(category, i);
            SkillAPCost cost = skill.GetComponent<SkillAPCost>();
            if (cost)
                menuOptions.Add(string.Format("{0}: {1}", skill.name, cost.amount));
            else
                menuOptions.Add(skill.name);
            locks[i] = skill.CanPerform();
        }
        menuOptions.Add("Back");
        actionMenuController.Show(menuTitle, menuOptions);
        for (int i = 0; i < count; ++i)
            if (turn.curActor.GetComponent<Enemy>() == null)
                actionMenuController.SetLocked(i, locks[i]);
            else
                actionMenuController.SetLocked(i, true);
    }
    protected override void Confirm()
    {
        actionMenuController.GetSelection();
        if (menuOptions[actionMenuController.selection] == "Back")
        {
            Cancel();
            return;
        }
        turn.skill = catalog.GetSkill(category, actionMenuController.selection);
        Manager.ChangeState<PerformActionState>();
    }
    protected override void Cancel()
    {
        Manager.ChangeState<SkillsCategoryMenuState>();
    }
}
