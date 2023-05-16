using UnityEngine;
using NPC;
using System;

public class Action_Idle : NPC_Action
{
    Animator animController;
    public override void Initialize<T>(T _data)
    {
        FieldInfo<object> idelInfo = _data as FieldInfo<object>;

        if (idelInfo == null)
            throw new Exception("Invalid Field Info");

        if (!idelInfo.gO)
            throw new Exception("No gameObjectPassedIn");

        animController = idelInfo.gO.GetComponent<Animator>();

        if (!animController)
            throw new NotImplementedException("No Animator Component");

        
    }

    public override void DoAction()
    {
        animController.Play("Idle");
    }
}
