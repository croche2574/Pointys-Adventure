using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // Called when state starts
    public virtual void Enter()
    {
        AddListeners();
    }

    // Called when state ends
    public virtual void Exit()
    {
        RemoveListeners();
    }

    // Called when state is destroyed
    protected virtual void OnDestroy()
    {

    }

    // Adds Event Listeners
    protected virtual void AddListeners()
    {

    }

    // Removes event listeners
    protected virtual void RemoveListeners()
    {
        
    }
}
