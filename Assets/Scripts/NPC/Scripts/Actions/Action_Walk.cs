using System;
using UnityEngine;
using NPC;

[Serializable]
public class Action_Walk : NPC_Action
{
    private Vector3 destination;
    private GameObject player;
    private Animator animController;

    public override void Initialize<T>(T _data)
    {
        FieldInfo<Vector3> movementInfo = _data as FieldInfo<Vector3>;
        player = movementInfo.gO;

        destination = movementInfo.data as Vector3? ?? default(Vector3);

        if (destination == null ||!player)
            throw new ArgumentException("Passed data was Invalid");

        animController = movementInfo.gO.GetComponent<Animator>();

        if (!animController)
            throw new NotImplementedException("No Animator Component");

        
    }
    public override void DoAction()
    {
        if(actionComplete) return;

        if (player.transform.position != destination)
        {
            animController.Play("Walk");
            Quaternion lookDirection = Quaternion.LookRotation(destination - player.transform.position);

            player.transform.rotation = lookDirection;

            player.transform.position = Vector3.MoveTowards(player.transform.position,destination,Time.deltaTime*1);
        }
        else
        {
            actionComplete = true;
        }
    }
}
