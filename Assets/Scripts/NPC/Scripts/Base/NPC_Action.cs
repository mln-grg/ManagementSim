using System;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public abstract class NPC_Action
    {
        protected bool actionComplete = false;
        public virtual bool IsActionCompleted()
        {
            return actionComplete;
        }

        public abstract void Initialize<T>(T _data);
        public abstract void DoAction();
    }
}