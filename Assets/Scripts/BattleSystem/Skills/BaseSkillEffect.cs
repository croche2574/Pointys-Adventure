using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillEffect : MonoBehaviour
{
    public const int minDamage = -999;
    public const int maxDamage = 999;
    public bool targetSelf = false;
    public const string GetAttackNotification = "BaseSkillEffect.GetAttackNotification";
    public const string GetDefenseNotification = "BaseSkillEffect.GetDefenseNotification";
    public const string GetPowerNotification = "BaseSkillEffect.GetPowerNotification";
    public const string TweakDamageNotification = "BaseSkillEffect.TweakDamageNotification";
    public const string HitNotification = "BaseSkillEffect.HitNotification";
    public abstract int Predict(Unit target);
    public void Apply(Unit target)
    {
        this.PostNotification(HitNotification, OnApply(target));
    }
    protected abstract int OnApply (Unit target);
    protected virtual int GetStat(Unit attacker, Unit target, string notification, int startValue)
    {
        var mods = new List<ValueModifier>();
        var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
        this.PostNotification(notification, info);
        mods.Sort(Compare);
        float value = startValue;
        for (int i = 0; i < mods.Count; ++i)
            value = mods[i].Modify(startValue, value);
        int retValue = Mathf.FloorToInt(value);
        retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
        return retValue;
    }
    int Compare (ValueModifier x, ValueModifier y)
	{
		return x.sortOrder.CompareTo(y.sortOrder);
	}
}
