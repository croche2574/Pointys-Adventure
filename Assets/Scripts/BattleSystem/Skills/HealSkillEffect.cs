using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkillEffect : BaseSkillEffect
{
    public StatTypes stat;
    public int adjust = 5;
    public override int Predict(Unit target)
    {
        return GetStat(target, target, GetPowerNotification, 0);
    }
    protected override int OnApply(Unit target)
    {
        int value = Predict(target);
        value /= adjust;
        value = Mathf.Clamp(value, minDamage, maxDamage);
        Stats s = target.GetComponent<Stats>();
        s[stat] += value;
        return value;
    }
}
