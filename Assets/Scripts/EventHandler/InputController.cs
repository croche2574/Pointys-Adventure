using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<int>> InventoryKeyEvent;
    public static event EventHandler<InfoEventArgs<int>> EscapeKeyEvent;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (InventoryKeyEvent != null)
            {
                InventoryKeyEvent(this, new InfoEventArgs<int>(0));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscapeKeyEvent != null)
            {
                EscapeKeyEvent(this, new InfoEventArgs<int>(0));
            }
        }
    }
}
