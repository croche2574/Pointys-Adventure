using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsCategoryMenuState : BaseActionMenuState
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
            menuOptions = new List<string>();
        else
            menuOptions.Clear();
        menuTitle = "Choose Type";
        SkillCatalog catalog = turn.curActor.GetComponentInChildren<SkillCatalog>();
        for (int i = 0; i < catalog.CategoryCount(); ++i)
            menuOptions.Add(catalog.GetCategory(i).name);
        menuOptions.Add("Back");
        actionMenuController.Show(menuTitle, menuOptions);
    }
    protected override void Confirm()
    {
        actionMenuController.GetSelection();
        if (menuOptions[actionMenuController.selection] == "Back")
        {
            Cancel();
            return;
        }
            
        SetCategory(actionMenuController.selection);
    }
    protected override void Cancel()
    {
        Manager.ChangeState<ChooseActionState>();
    }
    
    void SetCategory(int index)
    {
        SkillsMenuState.category = index;
        Manager.ChangeState<SkillsMenuState>();
    }
}
