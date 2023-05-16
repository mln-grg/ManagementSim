using NPC;
using System.Collections.Generic;
using UnityEngine;

public static class NPC_ActionMapping
{
    public static Dictionary<ActionType, System.Type> ActionMapping = new Dictionary<ActionType, System.Type>();

    public static System.Type GetAction(ActionType actionType)
    {
        return ActionMapping[actionType];
    }

    public static void AddMapping(ActionType key, System.Type value)
    {
        ActionMapping[key] = value; 
    }

}

