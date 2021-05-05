using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleData
{
    private static Vector3 startLoc;
    private static Quaternion startRot;
    public static Vector3 StartLoc 
    {
        get {return startLoc;} 
        set {startLoc = value;}
    }
    public static Quaternion StartRot
    {
        get {return startRot;} 
        set {startRot = value;}
    }
}
