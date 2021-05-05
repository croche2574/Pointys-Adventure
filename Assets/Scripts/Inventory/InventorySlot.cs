using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Update the slot to reflect the item within
public class InventorySlot : MonoBehaviour
{
    public GameObject item;
    public Image icon;
    InventoryHandler handler;
    public string itemDesc;

    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.FindObjectOfType<InventoryHandler>();
        icon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject newItem)
    {
        item = newItem;
        icon.sprite = item.GetComponent<SpriteRenderer>().sprite;
        icon.enabled = true;
        if (item.GetComponent<Consumable>() != null)
        {
            StatModifierFeature[] features = item.GetComponents<StatModifierFeature>();
            itemDesc = item.name;
            foreach (StatModifierFeature feature in features)
            {
                itemDesc += string.Format("\nRecovers {0} {1}", feature.amount, feature.type);
            }
            itemDesc += "\nConsume?";
        }
        else
        {
            StatModifierFeature feature = item.GetComponent<StatModifierFeature>();
            itemDesc = item.name;
            string type = (feature.type == StatTypes.ATK) ? "Attack" : "Defense";
            itemDesc += string.Format("\n+{0} {1}", feature.amount, type);
        }
    }

    public void DeleteItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClick()
    {
        handler.UseItem(item, this);
    }
}
