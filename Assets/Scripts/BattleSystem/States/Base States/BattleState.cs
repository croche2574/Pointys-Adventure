using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState : State
{
    protected BattleMachine Manager;
    public Turn turn {get {return Manager.turn;}}
    public Player player {
        get {return Manager.player;}
        set {Manager.player = value;}    
    }
    public Unit monster {
        get {return Manager.monster;}
        set {Manager.monster = value;}
    }
    public GameObject playerLoc {
        get {return Manager.playerLoc;}
        set {Manager.playerLoc = value;}
    }
    public GameObject monsterLoc {
        get {return Manager.monsterLoc;}
        set {Manager.monsterLoc = value;}
    }
    public ActionMenuController actionMenuController {
        get {return Manager.actionMenuController;}
    }
    public StatPanelController statPanelController {
        get {return Manager.statPanelController;}
    }
    protected virtual void Awake()
    {
        Manager = GetComponent<BattleMachine>();
    }

    protected override void AddListeners()
    {
        ActionMenuEntry.clickEvent += OnClickEvent;
    }

    protected override void RemoveListeners()
    {
        ActionMenuEntry.clickEvent -= OnClickEvent;
    }
    protected virtual void OnClickEvent(object sender, InfoEventArgs<string> e)
    {
        
    }
}
