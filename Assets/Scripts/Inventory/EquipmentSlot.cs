using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equippable item;
    public Image icon;

    public void AddItem(Equippable newItem)
    {
        item = newItem;
        icon.sprite = item.GetComponent<SpriteRenderer>().sprite;
        icon.enabled = true;
    }

    public void DeleteItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
