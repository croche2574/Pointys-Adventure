using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseATK : StatusEffect
{
    Unit owner;
    public float percentIncrease = .25f;
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
            AddValueModifier mod = new AddValueModifier(0, (1 + percentIncrease));
            exc.AddModifier(mod);
        }
    }
}
