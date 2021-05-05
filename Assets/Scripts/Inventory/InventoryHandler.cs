using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    Canvas[] inventory;
    Slider[] bars;
    Toggle[] skillPoints;
    public Button skillMenuButton;
    Text HP;
    Text AP;
    Text EXP;
    CanvasGroup interact;
    public Transform invSlotParent;
    public Transform eqpSlotParent;
    UseHandler use;
    InventorySlot[] invSlots;
    EquipmentSlot[] eqpSlots;
    Player hero;
    bool open = false;
    int index;
    Stats stats;

    GameObject savedPotion;
    GameObject savedThing;

    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponentsInChildren<Canvas>();
        hero = gameObject.GetComponentInParent<Player>();
        invSlots = invSlotParent.GetComponentsInChildren<InventorySlot>();
        eqpSlots = eqpSlotParent.GetComponentsInChildren<EquipmentSlot>();
        use = inventory[1].GetComponentInChildren<UseHandler>();
        interact = inventory[0].GetComponent<CanvasGroup>();
        bars = inventory[0].GetComponentsInChildren<Slider>();
        HP = bars[0].GetComponentInChildren<Text>();
        AP = bars[1].GetComponentInChildren<Text>();
        EXP = bars[2].GetComponentInChildren<Text>();
        skillPoints = inventory[2].GetComponentsInChildren<Toggle>();
        stats = hero.GetComponent<Stats>();
        if (inventory == null || hero == null || invSlots == null || eqpSlots == null || use == null || interact == null || bars == null)
        {
            Debug.Log("FUCK!");
        }
        inventory[0].enabled = false;
        inventory[1].enabled = false;
        inventory[2].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        skillMenuButton.interactable = (hero.battling) ? false : true;
        bars[0].maxValue = stats[StatTypes.MHP];
        bars[1].maxValue = stats[StatTypes.MAP];
        bars[2].maxValue = ExpHandler.ExperienceForLevel(stats[StatTypes.LVL] + 1);
        bars[0].value = stats[StatTypes.HP];
        bars[1].value = stats[StatTypes.AP];
        bars[2].value = stats[StatTypes.EXP];
        HP.text = stats[StatTypes.HP] + "/" + stats[StatTypes.MHP];
        AP.text = stats[StatTypes.AP] + "/" + stats[StatTypes.MAP];
        EXP.text = stats[StatTypes.EXP] + "/" + ExpHandler.ExperienceForLevel(stats[StatTypes.LVL] + 1);
    }

    //Present the inventory
    public void PresentInventory()
    {
        if (!open)
        {
            inventory[0].enabled = true;
            inventory[1].enabled = false;
            inventory[2].enabled = false;
            UpdateUI();
            open = true;
        }
        else
        {
            inventory[0].enabled = false;
            inventory[1].enabled = false;
            inventory[2].enabled = false;
            interact.interactable = true;
            UpdateUI();
            open = false;
        }
    }

    //Present the skill points
    public void PresentSkillPoints()
    {
        interact.interactable = false;
        inventory[2].enabled = true;
    }

    public void CloseSkillPoints()
    {
        interact.interactable = true;
        inventory[2].enabled = false;
    }

    //Update the UI to reflect the player's inventory
    public void UpdateUI()
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (i < hero.inventory.Count)
            {
                invSlots[i].AddItem(hero.inventory[i]);
            }
            else
            {
                invSlots[i].DeleteItem();
            }
        }
        eqpSlots[0].DeleteItem();
        eqpSlots[1].DeleteItem();
        Equippable[] equipped = hero.GetComponentsInChildren<Equippable>();
        foreach (Equippable e in equipped)
        {
            if (e.defaultSlots == EquipLocations.Armor)
            {
                eqpSlots[0].AddItem(e);
            }
            else
            {
                eqpSlots[1].AddItem(e);
            }
        }
    }

    //Check if you want to use an item
    public void UseItem(GameObject item, InventorySlot slot)
    {
        for (index = 0; index < invSlots.Length; index++)
        {
            if (invSlots[index].Equals(slot))
            {
                Debug.Log("We're the same slot!");
                if (item != null)
                {
                    inventory[1].enabled = true;
                    interact.interactable = false;
                    if (item.GetComponent<Consumable>() != null)
                    {
                        savedPotion = item;
                    }
                    else
                    {
                        savedThing = item;
                    }
                    use.ItemCheck(item, slot);
                    break;
                }
            }
        }
    }

    //Equip the item
    public void Equip()
    {
        if (savedThing.transform.parent == null)
                savedThing = Instantiate(savedThing) as GameObject;
        Equippable equip = savedThing.GetComponent<Equippable>();
        Equipment equipment = hero.GetComponent<Equipment>();
        if (equip.defaultSlots == EquipLocations.Weapon)
        {
            if (eqpSlots[1].item != null)
            {
                Debug.Log(eqpSlots[1].item.gameObject);
                hero.inventory.Add(eqpSlots[1].item.gameObject);
            }
        }
        else if (equip.defaultSlots == EquipLocations.Armor)
        {
            if (eqpSlots[0].item != null)
            {
                Debug.Log(eqpSlots[0].item.gameObject);
                hero.inventory.Add(eqpSlots[0].item.gameObject);
            }
        }
        equipment.Equip(equip, equip.defaultSlots);

        hero.inventory.RemoveAt(index);
        UpdateUI();
        inventory[1].enabled = false;
        interact.interactable = true;

    }

    //Consume the item
    public void Consume()
    {
        savedPotion.GetComponent<Consumable>().Consume(hero.gameObject);
        hero.inventory.RemoveAt(index);
        UpdateUI();
        inventory[1].enabled = false;
        interact.interactable = true;
    }

    //Cancel decision
    public void Cancel()
    {
        inventory[1].enabled = false;
        interact.interactable = true;
    }

    //Throw away the item
    public void Toss()
    {
        hero.inventory.RemoveAt(index);
        UpdateUI();
        inventory[1].enabled = false;
        interact.interactable = true;
    }

    //Update the hero's skill points
    public void UpdatePoints()
    {
        stats[StatTypes.FIRE] = 0;
        stats[StatTypes.WATER] = 0;
        stats[StatTypes.GRASS] = 0;
        for (int i = 0; i < skillPoints.Length; i++)
        {
            //Fire points
            if (i < 5 && skillPoints[i].isOn)
            {
                stats[StatTypes.FIRE]++;
            }
            //Water points
            else if (i < 10 && skillPoints[i].isOn)
            {
                stats[StatTypes.WATER]++;
            }
            //Grass points
            else if (i < 15 && skillPoints[i].isOn)
            {
                stats[StatTypes.GRASS]++;
            }
        }
    }
}
