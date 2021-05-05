using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkillPower : BaseSkillPower
{
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
        int power = PowerFromWeapon();
        return power > 0 ? power : UnarmedPower();
    }
    int PowerFromWeapon()
    {
        int power = 0;
        try 
        {
            Equipment eq = GetComponentInParent<Equipment>();
            Equippable item = eq.GetItem(EquipLocations.Weapon);
            StatModifierFeature[] features = item.GetComponentsInChildren<StatModifierFeature>();
            for (int i = 0; i  < features.Length; ++i)
            {
                if (features[i].type == StatTypes.ATK)
                    power += features[i].amount;
            }
        } catch {}
        return power;
    }
    int UnarmedPower()
    {
        Job job = transform.parent.GetComponentInChildren<Job>();
        for (int i = 0; i < Job.statOrder.Length; ++i)
        {
            if (Job.statOrder[i] == StatTypes.ATK)
                return job.baseStats[i];
        }
        return 0;
    }
}
