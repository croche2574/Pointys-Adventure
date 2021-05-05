using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The player is the parent of this item
//This handles all dialogue in the game when the player interacts with an interactable object
public class DialogueHandler : MonoBehaviour
{
    Canvas dialogue;
    Text stuffSaid;
    Player hero;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = this.gameObject.GetComponentInChildren<Canvas>();
        stuffSaid = dialogue.gameObject.GetComponentInChildren<Text>();
        hero = this.gameObject.GetComponentInParent<Player>();
        if (dialogue == null || stuffSaid == null || hero == null)
        {
            Debug.Log("That didn't work.");
        }
        dialogue.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PresentDialogue(string said)
    {
        dialogue.enabled = true;
        stuffSaid.text = said;
    }

    public void CloseDialogue()
    {
        dialogue.enabled = false;
        hero.talking = false;
        hero.anim.SetFloat("interact", 0);
    }
}
