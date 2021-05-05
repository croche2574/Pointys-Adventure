using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseATK : StatusEffect
{
    Unit owner;
    public float percentDecrease = .25f;
    Stats stats;
    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnGetAttack, BaseSkillEffect.GetAttackNotification);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnGetAttack, BaseSkillEffect.GetAttackNotification);
    }
    void OnGetAttack(object sender, object args)
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
