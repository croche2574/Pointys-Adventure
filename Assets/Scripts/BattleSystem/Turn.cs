using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Unit curActor;
    public bool hasUnitActed;
    public bool lockMove;
    public Unit target;
    public Skill skill;

    public void ChangeUnit(Unit actor)
    {
        target = curActor;
        curActor = actor;
        hasUnitActed = false;
        lockMove = true;
    }
}
