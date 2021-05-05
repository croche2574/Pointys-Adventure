using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDEF : StatusEffect
{
    Unit owner;
    public float percentDecrease = .25f;
    Stats stats;
    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnGetDefense, BaseSkillEffect.GetDefenseNotification);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnGetDefense, BaseSkillEffect.GetDefenseNotification);
    }
    void OnGetDefense(object sender, object args)
    {
        Unit s = sender as Unit;
        if (s == owner)
        {
            ValueChangeException exc = args as ValueChangeException;
            MultValueModifier mod = new MultValueModifier(0, (1 - percentDecrease));
            exc.AddModifier(mod);
        }
    }
}
