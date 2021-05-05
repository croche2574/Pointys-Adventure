using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Display a text box when the player interacts
public class NPC : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Interact(Player player)
    {
        return dialogue;
    }
}
