using System;
using System.Collections.Generic;
using System.Linq;

namespace NPC
{
    [Serializable]
    public class NPC_State
    {
        protected List<NPC_Action> actionList = new List<NPC_Action>();

        public virtual void OnStateEnter<T>(Type actionType , T data)
        {
            AddAction<T>(actionType,data);
            OnStateEnabled();
        }

        public virtual void OnStateEnabled()
        {
           
        }

        public virtual void StateUpdate()
        {
            if (actionList.Count > 0)
            {
                foreach (var action in actionList.ToList<NPC_Action>())
                {
                    PerformAction(action);
                    if (action.IsActionCompleted())
                        actionList.Remove(action);
                }
            }
        }

        public virtual void PerformAction(NPC_Action action)
        {
            action.DoAction();
        }

        public virtual void OnStateDisabled()
        {

        }

        public virtual void OnStateExit()
        {
            OnStateDisabled();

            actionList.Clear();
        }
        public virtual bool IsStateOver()
        {
            if(actionList.Count > 0)
            {
                return false;
            }

            return true;
        }
        public virtual void AddAction<T>(Type actionType, T data)
        {
            NPC_Action action = CreateAction<T>(actionType, data);

            if (action== null)
                throw new ArgumentNullException("Action is Null");

            if(!actionList.Contains(action))
                actionList.Add(action);
        }

        private NPC_Action CreateAction<T>(Type actionType , T data)
        {
            Object actionObj = Activator.CreateInstance(actionType);

            NPC_Action action = actionObj as NPC_Action;

            if(action == null)
                throw new InvalidCastException("Passed in Action Type was Invalid!");

            action.Initialize<T>(data);

            return action;
               
        }
        
    }
}