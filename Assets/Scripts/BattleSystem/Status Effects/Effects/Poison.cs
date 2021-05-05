using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    Unit owner;
    public float amt = .1f;
    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification);
    }
    void OnNewTurn(object sender, object args)
    {
        BattleMachine m = FindObjectOfType<BattleMachine>();
        Stats s = GetComponentInParent<Stats>();
        int currentHP = s[StatTypes.HP];
        int maxHP = s[StatTypes.MHP];
        int reduce = Mathf.Min(currentHP, Mathf.FloorToInt(maxHP * amt));
        if (m.turn.curActor.GetComponent<Enemy>() == null)
        {
            s.SetValue(StatTypes.HP, (currentHP - reduce), false);
        }
        else if (m.turn.curActor.GetComponent<Enemy>().GetComponent<Stats>()[StatTypes.HP] > reduce)
        {
            s.SetValue(StatTypes.HP, (currentHP - reduce), false);
        }
    }
}
