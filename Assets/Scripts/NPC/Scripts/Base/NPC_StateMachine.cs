using UnityEngine;
using NPC;
using System.Collections.Generic;
using System;

public class NPC_StateMachine : MonoBehaviour
{
    [SerializeField] private Stack<NPC_State> states = new Stack<NPC_State>();
    [SerializeField] private NPC_State currentState;


    public StateType GetCurrentState()
    {
        if(currentState!=null)
            return currentState.type;

        return StateType.Empty;
    }

    public void SetCurrentState<T>(StateType stateType, ActionType actionType, T data)
    {
        NPC_State state;

       switch (stateType)
        {
            case StateType.Idle:
                state = new NPCState_Idle();
                break;
            case StateType.Travel:
                state = new NPCState_Travel();
                break;
            case StateType.Task:
                state = new NPCState_PerformTask();
                break;
            default:
                throw new Exception("Invalid State Transition!");
        }

        if (currentState != null)
        {
            currentState.OnStateDisabled();

            currentState= null;
        }

        states.Push(state);

        currentState= state;

        currentState.OnStateEnter<T>(stateType,actionType,data);
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
    public void AddActionToCurrentState<T>(ActionType actionType, T data)
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
            throw new ArgumentException("Trying to remove Idle State");
        }

        currentState.OnStateExit();
        states.Pop();
        currentState= states.Peek();
        currentState.OnStateEnabled();
        
    }

}
