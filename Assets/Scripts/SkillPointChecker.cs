using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointChecker : MonoBehaviour
{
    public Toggle parent;
    Player hero;
    Toggle self;
    Toggle child;
    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject.GetComponent<Toggle>();
        hero = this.gameObject.GetComponentInParent<Player>();
        stats = hero.GetComponent<Stats>();
        if(parent == null)
        {
            child = gameObject.GetComponentsInChildren<Toggle>()[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(parent != null)
        {
            if (!parent.isOn)
            {
                self.interactable = false;
                self.isOn = false;
            }
            else
            {
                self.interactable = true;
                if (stats[StatTypes.SP] == 0 && !self.isOn)
                {
                    self.isOn = false;
                    self.interactable = false;
                }
            }
        }
        else
        {
            self.interactable = true;
            if (stats[StatTypes.SP] == 0 && !child.isOn && !self.isOn)
            {
                self.isOn = false;
                self.interactable = false;
            }
        }
    }

    public void PointsManager()
    {
        if (self.isOn)
        {
            stats[StatTypes.SP]--;
        }
        else
        {
            stats[StatTypes.SP]++;
        }
    }
    
}
