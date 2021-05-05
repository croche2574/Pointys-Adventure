using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSkillPower : BaseSkillPower
{
    public float levelAdjust = .5f;
    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.ATK];
    }
    protected override int GetBaseDefense(Unit target)
    {
        return target.GetComponentInParent<Stats>()[StatTypes.DEF];
    }
    protected override int GetPower()
    {
        return Mathf.FloorToInt(GetComponentInParent<Stats>()[StatTypes.LVL] * levelAdjust);
    }
}
