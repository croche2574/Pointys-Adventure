using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAPCost : MonoBehaviour
{
    public int amount;
    Skill owner;

    void Awake()
    {
        owner = GetComponent<Skill>();
    }
    void OnEnable ()
    {
        this.AddObserver(OnCanPerformCheck, Skill.CanPerformCheck, owner);
        this.AddObserver(OnDidPerformNotification, Skill.DidPerformNotification, owner);
    }
    void OnDisable ()
    {
        this.RemoveObserver(OnCanPerformCheck, Skill.CanPerformCheck, owner);
        this.RemoveObserver(OnDidPerformNotification, Skill.DidPerformNotification, owner);
    }
    void OnCanPerformCheck(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        if (s[StatTypes.AP] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.flipToggle();
        }
    }
    void OnDidPerformNotification(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        Debug.Log("Ap deduction: " + amount);
        s[StatTypes.AP] -= amount;
    }
}
