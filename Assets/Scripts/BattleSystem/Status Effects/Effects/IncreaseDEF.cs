using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDEF : StatusEffect
{
    Unit owner;
    public float percentIncrease = .25f;
    Stats stats;
    StatTypes statType;
    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnGetDefense, BaseSkillEffect.GetDefenseNotification);
    }
    void OnDisable()
    {
        this.AddObserver(OnGetDefense, BaseSkillEffect.GetDefenseNotification);
    }
    void OnGetDefense(object sender, object args)
    {
        Unit s = sender as Unit;
        if (s == owner)
        {
            ValueChangeException exc = args as ValueChangeException;
            MultValueModifier mod = new MultValueModifier(0, (1 + percentIncrease));
            exc.AddModifier(mod);
        }
    }
}
