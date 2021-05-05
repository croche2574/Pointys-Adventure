using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatPanelController : MonoBehaviour
{
    const string ShowKey = "Show";
    const string Hidekey = "Hide";

    [SerializeField] StatPanel playerPanel;
    [SerializeField] StatPanel mobPanel;
    private string curTurn = "Player";
    private bool toggle = true;
    Tweener playerTransition;
    Tweener mobTransition;
    BattleMachine b;

    void Start()
    {
        if (playerPanel.panel.CurrentPosition == null)
            playerPanel.panel.SetPosition(Hidekey, false);
        if (mobPanel.panel.CurrentPosition == null)
            mobPanel.panel.SetPosition(Hidekey, false);
        b = FindObjectOfType<BattleMachine>();
        
    }
    public void ShowPlayer(Unit obj)
    {
        if (b.turn.curActor.GetComponent<Enemy>() == null)
            curTurn = "Player";
        else
            curTurn = "Mob";
        playerPanel.isTurn = (curTurn == "Player") ? true : false;
        playerPanel.Display(obj);
        MovePanel(playerPanel, ShowKey, ref playerTransition);
    }
    public void HidePlayer()
    {
        MovePanel(playerPanel, Hidekey, ref playerTransition);
    }
    public void ShowMob(Unit obj)
    {
        mobPanel.isTurn = (curTurn == "Mob") ? true : false;
        mobPanel.Display(obj);
        MovePanel(mobPanel, ShowKey, ref mobTransition);
    }
    public void HideMob()
    {
        MovePanel(mobPanel, Hidekey, ref mobTransition);
    }
    public void ToggleTurn()
    {
        toggle = !toggle;
        curTurn = (toggle) ? "Player" : "Mob";
    }
    void MovePanel(StatPanel obj, string pos, ref Tweener t)
    {
        Panel.Position target = obj.panel[pos];
        if (obj.panel.CurrentPosition != target)
        {
            if (t != null && t.easingControl != null)
                t.easingControl.Stop();
            t = obj.panel.SetPosition(pos, true);
            t.easingControl.duration = 0.5f;
            t.easingControl.equation = EasingEquations.EaseOutQuad;
        }
    }
}
