using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public const string CanPerformCheck = "Skill.CanPerformCheck";
    public const string FailedNotification = "Skill.FailedNotification";
    public const string DidPerformNotification = "Skill.DidPerformNotification";
    public bool CanPerform()
    {
        BaseException exc = new BaseException(true);
        this.PostNotification(CanPerformCheck, exc);
        return exc.toggle;
    }
    public void Perform(Unit target)
    {
        if (!CanPerform())
        {
            Debug.Log("Ability failed");
            this.PostNotification(FailedNotification);
            return;
        }
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            BaseSkillEffect effect = child.GetComponent<BaseSkillEffect>();
            if (effect.targetSelf)
                effect.Apply(transform.root.GetComponent<Unit>());
            else
                effect.Apply(target);
        }
        this.PostNotification(DidPerformNotification);

    }
    
}
