using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkillPower : BaseSkillPower
{
    public StatTypes element;
    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.ATK];
    }
    protected override int GetBaseDefense(Unit target)
    {
        return GetComponentInParent<Stats>()[StatTypes.DEF];
    }
    protected override int GetPower()
    {
        int power = ElementalPower();
        return power;
    }
    int ElementalPower()
    {
        int power = 0;
        Stats s = GetComponentInParent<Stats>();
        power = s[element];
        return power;
    }
}
