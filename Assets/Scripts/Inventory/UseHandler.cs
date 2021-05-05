using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseHandler : MonoBehaviour
{
    Button[] buttons;
    Text mainText;

    // Start is called before the first frame update
    void Start()
    {
        buttons = this.gameObject.GetComponentsInChildren<Button>();
        //buttons[0] is the use button
        //buttons[1] is the equip button
        //buttons[2] is the cancel button
        //buttons[3] is the throw button
        mainText = this.gameObject.GetComponentInChildren<Text>();
        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
    }

    public void ItemCheck(GameObject item, InventorySlot slot)
    {
        if(item.GetComponent<Consumable>() != null)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(false);
        }
        else
        {
            buttons[1].gameObject.SetActive(true);
            buttons[0].gameObject.SetActive(false);
        }
        mainText.text = slot.itemDesc;
    }
}
