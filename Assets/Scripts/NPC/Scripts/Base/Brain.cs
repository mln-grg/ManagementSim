using UnityEngine;
using CustomGameEvents;
using System.Collections.Generic;
using NPC;
using System;

[RequireComponent(typeof(NPC_StateMachine))]
public class Brain : MonoBehaviour
{
    private NPC_StateMachine stateMachineRef;
    [SerializeField] private Material playerMaterial;

    private void Awake()
    {
        stateMachineRef= GetComponent<NPC_StateMachine>();  
    }

   

    private void OnEnable()
    {
        FieldInfo<Material> materialInfo = new FieldInfo<Material>(this.gameObject, playerMaterial);
        RequestStateChange(StateType.Idle, ActionType.Idle, materialInfo);
    }

    public void RequestStateChange<T>(StateType stateType, ActionType actionType , T data)
    {
        stateMachineRef.SetCurrentState(stateType, actionType , data);
    }
}
