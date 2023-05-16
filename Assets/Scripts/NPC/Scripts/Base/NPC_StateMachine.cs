using UnityEngine;
using NPC;
using System.Collections.Generic;
using System;

public class NPC_StateMachine : MonoBehaviour
{
    [SerializeField] private Stack<NPC_State> states = new Stack<NPC_State>();
    [SerializeField] private string CurrentState;

    private NPC_State currentState;
    public string GetCurrentStateType()
    {
       return CurrentState;
    }

    public void SetCurrentState<T>(Type stateType, Type actionType, T data)
    {
        object stateObj = Activator.CreateInstance(stateType);

        NPC_State state = stateObj as NPC_State;

        if (state == null)
            throw new InvalidCastException("Passed in State Type was Invalid");


        if (currentState != null)
        {
            currentState.OnStateDisabled();

            currentState= null;
        }

        states.Push(state);

        currentState= state;

        CurrentState= currentState.ToString();

        currentState.OnStateEnter<T>(actionType,data);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();

            if (currentState.IsStateOver())
                RemoveCurrentState();
        }
        
    }
    public void AddActionToCurrentState<T>(Type actionType, T data)
    {
        if(currentState!= null)
        {
            currentState.AddAction(actionType, data);
        }
    }

    public void RemoveCurrentState()
    {
        if(states.Count ==1) 
        {
            throw new ArgumentException("Trying to remove Last State");
        }

        currentState.OnStateExit();
        states.Pop();
        currentState= states.Peek();
        currentState.OnStateEnabled();
        
    }

}
