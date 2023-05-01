using System;
using UnityEngine;
using NPC;
public class Action_Walk : NPC_Action
{
    private Vector3 destination;
    private GameObject player;

    public override void Initialize<T>(T _data)
    {
        FieldInfo<Vector3> movementInfo = _data as FieldInfo<Vector3>;
        player = movementInfo.gO;

        destination = movementInfo.data as Vector3? ?? default(Vector3);

        if (destination == null ||!player)
            throw new ArgumentException("Passed data was Invalid");
    }
    public override void DoAction()
    {
        if (player.transform.position != destination)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position,destination,Time.deltaTime*5f);
        }
        else
        {
            actionComplete = true;
        }
    }
}
