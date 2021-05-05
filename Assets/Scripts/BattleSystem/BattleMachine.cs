using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMachine : StateMachine
{
    public Turn turn = new Turn();
    public Player player;
    public Unit monster;
    public ActionMenuController actionMenuController;
    public StatPanelController statPanelController;
    public GameObject skillMenu;
    public GameObject playerLoc;
    public GameObject monsterLoc;
    public List<Unit> units;
    public IEnumerator round;
    void Start()
    {
        ChangeState<InitBattleState>();
    }
}
