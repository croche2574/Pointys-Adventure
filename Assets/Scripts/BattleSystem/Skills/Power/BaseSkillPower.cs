using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillPower : MonoBehaviour
{
    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Unit target);
    protected abstract int GetPower();

    void OnEnable ()
    {
        this.AddObserver(OnGetBaseAttack, DamageSkillEffect.GetAttackNotification);
        this.AddObserver(OnGetBaseDefense, DamageSkillEffect.GetDefenseNotification);
        this.AddObserver(OnGetPower, DamageSkillEffect.GetPowerNotification);
    }
    void OnDisable ()
    {
        this.RemoveObserver(OnGetBaseAttack, DamageSkillEffect.GetAttackNotification);
        this.RemoveObserver(OnGetBaseDefense, DamageSkillEffect.GetDefenseNotification);
        this.RemoveObserver(OnGetPower, DamageSkillEffect.GetPowerNotification);
    }
    void OnGetBaseAttack(object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
        AddValueModifier mod = new AddValueModifier(0, GetBaseAttack());
        info.arg2.Add(mod);
    }
    void OnGetBaseDefense(object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
        AddValueModifier mod = new AddValueModifier(0, GetBaseDefense(info.arg1));
        info.arg2.Add(mod);
    }
    void OnGetPower(object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
        AddValueModifier mod = new AddValueModifier(0, GetPower());
        info.arg2.Add(mod);
    }
}
