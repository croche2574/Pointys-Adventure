using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipment : MonoBehaviour
{
    public const string EquippedNotification = "Equipment.EquippedNotification";
    public const string UnEquippedNotification = "Equipment.UnEquippedNotification";

    public IList<Equippable> items{get {return _items.AsReadOnly();}}
    List<Equippable> _items = new List<Equippable>();

    public void Equip(Equippable item, EquipLocations slots)
    {
        UnEquip(slots);
        _items.Add(item);
        item.transform.SetParent(transform);
        item.slots = slots;
        item.OnEquip();
        this.PostNotification(EquippedNotification, item);
    }
    public void UnEquip(Equippable item)
    {
        Debug.Log("Unequip2");
        item.OnUnequip();
        item.slots = EquipLocations.None;
        item.transform.SetParent(transform);
        _items.Remove(item);
        this.PostNotification(UnEquippedNotification, item);
    }
    public void UnEquip(EquipLocations slots)
    {
        
        for(int i = _items.Count - 1; i >= 0; --i)
        {
            Equippable item = _items[i];
            Debug.Log("Unequip1");
            if ((item.slots & slots) != EquipLocations.None)
                UnEquip(item);
        }
    }
    public Equippable GetItem(EquipLocations slots)
    {
        for (int i = _items.Count - 1; i >= 0; --i)
        {
            Equippable item = _items[i];
            if ((item.slots & slots) != EquipLocations.None)
                return item;
        }
        return null;
    }
}
