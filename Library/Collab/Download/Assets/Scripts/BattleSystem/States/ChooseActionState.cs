using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionState : BaseActionMenuState
{
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
        if(menuOptions == null)
        {
            menuTitle = "Choose Action";
            menuOptions = new List<string>(4);
            menuOptions.Add("Attack");
            menuOptions.Add("Skills");
            menuOptions.Add("Items");
            menuOptions.Add("Run");
        }
        actionMenuController.Show(menuTitle, menuOptions);
    }
    protected override void Confirm()
    {
        actionMenuController.GetSelection();
        switch(actionMenuController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                Manager.ChangeState<SkillsCategoryMenuState>();
                break;
            case 2:
                Manager.ChangeState<InventoryState>();
                break;
            case 3:
                Cancel();
                break;
        }
    }
    protected override void Cancel()
    {
        Manager.ChangeState<BattleEndState>();
    }
    void Attack()
    {
        turn.skill = turn.curActor.GetComponentInChildren<Skill>();
        Manager.ChangeState<PerformActionState>();
    }
}
