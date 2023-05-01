using UnityEngine;
using NPC;
using System;

public class Action_Idle : NPC_Action
{
    private Material PlayerMaterial;
    private float nextInterval;
    public override void Initialize<T>(T _data)
    {
        FieldInfo<Material> matInfo = _data as FieldInfo<Material>; 
        PlayerMaterial = matInfo.data as Material;

        if(PlayerMaterial == null)
        {
            throw new ArgumentException("Passed data to action is invalid");
        }
    }

    public override void DoAction()
    {
        if(nextInterval<0)
        {
            PlayerMaterial.color = UnityEngine.Random.ColorHSV();
            nextInterval = 0.5f;
        }
        nextInterval-=Time.deltaTime;
    }
}
