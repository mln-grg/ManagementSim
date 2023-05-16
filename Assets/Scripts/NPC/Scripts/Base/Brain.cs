using UnityEngine;
using NPC;
using System;

[RequireComponent(typeof(NPC_StateMachine))]
public class Brain : MonoBehaviour
{
    private NPC_StateMachine stateMachineRef;
    [SerializeField] private NPCType npcType;

    private void Awake()
    {
        stateMachineRef= GetComponent<NPC_StateMachine>();  
    }

   

    private void OnEnable()
    {
        FieldInfo<object> emptyInfo = new FieldInfo<object>(this.gameObject,null);
        RequestStateChange(typeof(Settler_Idle), typeof(Action_Idle), emptyInfo);
    }

    public void RequestStateChange<T>(Type stateType, Type actionType , T data)
    {
        stateMachineRef.SetCurrentState(stateType, actionType , data);
    }
}
