using System;
using System.Collections.Generic;

namespace NPC
{
    [Serializable]
    public class NPC_State
    {
        protected List<NPC_Action> Actions = new List<NPC_Action>();

        protected event Action RemoveState = delegate { };
        protected Action subscriber;
        public virtual void OnStateEnter(Action action)
        {

            OnStateEnabled(action);
        }

        public virtual void OnStateEnabled(Action action)
        {
            subscriber= action;

            if (subscriber == null)
                throw new ArgumentNullException("Subscriber to state event is null");

            RemoveState += subscriber;
        }
        
        public virtual void AddAction(NPC_Action action)
        {
            if(action== null)
                throw new ArgumentNullException("Action is Null");

            if(!Actions.Contains(action)) 
                Actions.Add(action);
        }
        public virtual void StateUpdate()
        {
            if(Actions.Count > 0)
                foreach (var action in Actions)
                    PerformAction(action);
            else
            {
                //Is this thing going to invoke more than once before the state is removed ??
                RemoveState.Invoke();
            }
        }

        public virtual void RemoveAction(NPC_Action action)
        {
            if (!Actions.Contains(action))
            {
                throw new ArgumentException("Invalid Action Remove Operation");
            }

            Actions.Remove(action);
        }

        public virtual void PerformAction(NPC_Action action)
        {
            action.DoAction();
        }

        public virtual void OnStateDisabled()
        {
            if (subscriber == null)
                throw new ArgumentNullException("Subscriber to state event is null");
            
            RemoveState -= subscriber;

        }

        public virtual void OnStateExit()
        {
            OnStateDisabled();

            Actions.Clear();
        }

    }
}