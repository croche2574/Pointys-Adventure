using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : BattleState
{
    protected override void AddListeners()
    {
        base.AddListeners();
        InputController.InventoryKeyEvent += OnInventoryKeyEvent;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.InventoryKeyEvent -= OnInventoryKeyEvent;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Inventory State");
        player.Inventory();
    }

    public override void Exit()
    {
        base.Exit();
        Manager.ChangeState<ChooseActionState>();
    }
    void OnInventoryKeyEvent(object sender, InfoEventArgs<int> e)
    {
        player.Inventory();
        Exit();
    }
}
