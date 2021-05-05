using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSkillEffect : BaseSkillEffect
{
    public EffectList effect;
    public int duration;
    public override int Predict(Unit target)
    {
        return 0;
    }
    protected override int OnApply(Unit target)
    {
        if (effect == EffectList.Poison)
            Add<Poison>(target, duration);
        else if (effect == EffectList.DecreaseATK)
            Add<DecreaseATK>(target, duration);
        else if (effect == EffectList.DecreaseDEF)
            Add<DecreaseDEF>(target, duration);
        else if (effect == EffectList.IncreaseATK)
            Add<IncreaseATK>(target, duration);
        else if (effect == EffectList.IncreaseDEF)
            Add<IncreaseDEF>(target, duration);
        return 0;
    }
    void Add<T>(Unit target, int duration) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>();
        condition.duration = duration;
    }
}
