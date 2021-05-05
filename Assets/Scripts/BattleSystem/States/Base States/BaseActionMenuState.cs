using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionMenuState : BattleState
{
    protected string menuTitle;
    protected List<string> menuOptions;

    protected override void AddListeners()
    {
        base.AddListeners();
        InputController.EscapeKeyEvent += OnEscapeKeyEvent;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.EscapeKeyEvent -= OnEscapeKeyEvent;
    }

    public override void Enter()
    {
        base.Enter();
        LoadMenu();
    }
    public override void Exit()
    {
        base.Exit();
        actionMenuController.Hide();
    }
    protected override void OnClickEvent(object sender, InfoEventArgs<string> e)
    {
        Confirm();
    }
    protected void OnEscapeKeyEvent(object sender, InfoEventArgs<int> e)
    {
        Cancel();
    }
    protected abstract void LoadMenu();
    protected abstract void Confirm();
    protected abstract void Cancel();
}
