using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public Panel panel;
    public Text unitName;
    public Slider HealthBar;
    public Slider AttackBar;
    public Text HP;
    public Text AP;
    public Image turnIndicator;
    public bool isTurn;
    public Image grass;
    public Image fire;
    public Image water;

    public void Display(Unit obj)
    {
        Stats stats = obj.GetComponent<Stats>();
        string name = obj.name.Replace("(Clone)","").Trim();
        unitName.text = string.Format("Lvl. {0} {1}", stats[StatTypes.LVL], name);
        if (stats)
        {
            grass.enabled = (stats[StatTypes.GRASS] >= 1) ? true : false;
            fire.enabled = (stats[StatTypes.FIRE] >= 1) ? true : false;
            water.enabled = (stats[StatTypes.WATER] >= 1) ? true : false;
            turnIndicator.enabled = isTurn;
            HealthBar.maxValue = stats[StatTypes.MHP];
            AttackBar.maxValue = stats[StatTypes.MAP];
            HealthBar.value = stats[StatTypes.HP];
            AttackBar.value = stats[StatTypes.AP];
            HP.text = stats[StatTypes.HP] + "/" + stats[StatTypes.MHP];
            AP.text = stats[StatTypes.AP] + "/" + stats[StatTypes.MAP];
        }
        
    }
}
