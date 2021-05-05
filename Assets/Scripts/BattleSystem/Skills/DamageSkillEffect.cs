using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkillEffect : BaseSkillEffect
{
    public int powerEffect = 5;
    public override int Predict(Unit target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target;
        int attack = GetStat(attacker, defender, GetAttackNotification, 0);
        int defense = GetStat(attacker, defender, GetDefenseNotification, 0);
        //Debug.Log("Attack: " + attack + " Defense: " + defense);
        int damage = attack - (defense / 2);
        //Debug.Log("Raw damage: " + damage);
        damage = Mathf.Max(damage, 1);
        int power = GetStat(attacker, defender, GetPowerNotification, 0);
        //Debug.Log("Power:" + power);
        damage = power * damage / powerEffect;
        damage = Mathf.Max(damage, 1);
        //Debug.Log("Pure damage " + damage);
        damage = GetStat(attacker, defender, TweakDamageNotification, damage);
        damage = Mathf.Clamp(damage, minDamage, maxDamage);
        //Debug.Log("Processed damage " + damage);
        return damage;
    }

    protected override int OnApply(Unit target)
    {
        Unit defender = target;
        int value = Predict(defender);
        value = Mathf.Clamp(value, minDamage, maxDamage);
        //Debug.Log("Damage: " + value);
        Stats stats = defender.GetComponent<Stats>();
        stats[StatTypes.HP] -= value;
        return value;
    }
}
