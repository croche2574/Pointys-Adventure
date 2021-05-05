using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoints : MonoBehaviour
{
    public int AP
    {
        get {return stats[StatTypes.AP];}
        set {stats[StatTypes.AP] = value;}
    }
    public int MAP
    {
        get {return stats[StatTypes.MAP];}
        set {stats[StatTypes.MAP] = value;}
    }
    Unit unit;
    Stats stats;

    void Awake()
    {
        stats = GetComponent<Stats>();
        unit = GetComponent<Unit>();
    }
    void OnEnable ()
    {
        this.AddObserver(OnAPWillChange, Stats.WillChangeNotification(StatTypes.AP), stats);
        this.AddObserver(OnMAPDidChange, Stats.DidChangeNotification(StatTypes.MAP), stats);
        this.AddObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification, unit);
    }
    
    void OnDisable ()
    {
        this.RemoveObserver(OnAPWillChange, Stats.WillChangeNotification(StatTypes.AP), stats);
        this.RemoveObserver(OnMAPDidChange, Stats.DidChangeNotification(StatTypes.MAP), stats);
        this.RemoveObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification, unit);
    }
    void OnAPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, 0, stats[StatTypes.MAP]));
    }
    void OnMAPDidChange(object sender, object args)
    {
        int oldMap = (int)args;
        if (MAP > oldMap)
            AP += MAP - oldMap;
        else
            AP = Mathf.Clamp(AP, 0, MAP);
    }
    void OnTurnBegan(object sender, object args)
    {

    }
}