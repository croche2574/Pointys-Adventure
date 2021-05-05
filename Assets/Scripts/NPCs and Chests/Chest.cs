using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    //When the chest is interacted with, play an animation and display dialogue
    Animator anim;
    public GameObject treasure;
    bool opened = false;

    private static int chestID = 0;
    public int thisID = -1;
    public static Dictionary<int, bool> openedChests;

    private void Reset()
    {
        thisID = chestID;
        chestID++;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(treasure.name[0] == 'A' || treasure.name[0] == 'E' || treasure.name[0] == 'I' || treasure.name[0] == 'O' || treasure.name[0] == 'U')
        {
            dialogue = "You found an " + treasure.name + "!";
        }
        else
        {
            dialogue = "You found a " + treasure.name + "!";
        }
        anim = this.gameObject.GetComponent<Animator>();
        if (openedChests.ContainsKey(thisID))
        {
            if (openedChests[thisID])
            {
                opened = true;
            }
            else
            {
                openedChests.Add(thisID, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {
            anim.SetFloat("Opened", 1);
        }
    }

    private void Awake()
    {
        if(openedChests == null)
        {
            openedChests = new Dictionary<int, bool>();
        }
    }

    //Interact is called when the player interacts with the chest
    public string Interact(Player player)
    {
        if (!opened)
        {
            Debug.Log("I'm being interacted with!");
            anim.SetFloat("Opened", 1);
            player.inventory.Add(treasure);
            openedChests[thisID] = true;
            opened = true;
            return dialogue;
        }
        else
        {
            return "The chest is empty...";
        }
    }
}
