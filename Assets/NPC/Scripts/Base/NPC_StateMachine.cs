using UnityEngine;
using NPC;
using System.Collections.Generic;
using System;

public class NPC_StateMachine : MonoBehaviour
{
    [SerializeField] private Stack<NPC_State> states;
    [SerializeField] private NPC_State currentState;


    public void SetCurrentState(NPC_State state, NPC_Action action)
    {
        if(state == null)
            throw new ArgumentNullException("Trying to set Null State");

        if (currentState != null)
        {
            currentState.OnStateDisabled();

            currentState= null;
        }

        state = new NPC_State();
        state.AddAction(action);

        states.Push(state);

        currentState= state;

        currentState.OnStateEnter(RemoveCurrentState);
    }

    public void RemoveCurrentState()
    {
        if(states.Count ==1) 
        {
            throw new ArgumentException("Trying to remove Idle State");
        }

        currentState.OnStateExit();
        states.Pop();
        currentState= states.Peek();
        currentState.OnStateEnabled(RemoveCurrentState);
        
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
    }
}
