using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExperienceManager
{
    public static void AwardExperience(int amount, Unit player)
    {
        ExpHandler expHandler = player.GetComponent<ExpHandler>();
        expHandler.EXP += Mathf.FloorToInt(amount / 3);
    }
}
