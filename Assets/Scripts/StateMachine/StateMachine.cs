using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get{return _currentState;}
        set{Transition(value);}
    }

    protected State _currentState;
    protected bool _inTransition;

    public virtual T GetState<T>() where T : State
    {
        // Check if a state exists and returns it. If none exists one is created.
        T target = GetComponent<T>();
        if(target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }

    // Can only accept state as a type
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    protected virtual void Transition(State state)
    {
        if(_currentState == state || _inTransition)
        {
            return;
        }
        _inTransition = true;

        if(_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = state;

        if(_currentState != null)
        {
            _currentState.Enter();
        }
        _inTransition = false;
    }
}
