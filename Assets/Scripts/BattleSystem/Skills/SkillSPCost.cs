using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSPCost : MonoBehaviour
{
    public int amount;
    public StatTypes element;
    Skill owner;
    // Start is called before the first frame update
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
        if (s[element] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.flipToggle();
        }
    }
    void OnDidPerformNotification(object sender, object args)
    {
        
    }
}
