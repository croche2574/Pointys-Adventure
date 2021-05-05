using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionMenuEntry : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite disabledSprite;
    [SerializeField] Text label;
    public static event EventHandler<InfoEventArgs<string>> clickEvent;

    public string Title
    {
        get {return label.text;}
        set {label.text = value;}
    }

    [System.Flags]
    enum States
    {
        None = 0,
        Selected = 1 << 0,
        Locked = 1 << 1
    }
    public bool IsLocked
    {
        get {return (State & States.Locked) != States.None;}
        set
        {
            if (value)
                State |= States.Locked;
            else
                State &= ~States.Locked;
            button.interactable = value;
        }
    }
    public bool IsSelected
    {
        get {return (State & States.Selected) != States.None;}
        set
        {
            if (value)
                State |= States.Selected;
            else
                State &= ~States.Selected;
        }
    }
    States State
    {
        get {return state;}
        set
        {
            if (state == value)
                return;
            state = value;
        }
    }
    States state;

    public void Reset()
    {
        State = States.None;
        button.interactable = true;
    }

    public void OnClick()
    {
        IsSelected = true;
        if (clickEvent != null)
        {
            clickEvent(this, new InfoEventArgs<string>("Clicked"));
        }
    }
}
