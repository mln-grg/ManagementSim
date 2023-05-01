using System;
using System.Collections.Generic;
using System.Linq;

namespace NPC
{
    [Serializable]
    public class NPC_State
    {
        public StateType type;

        protected List<NPC_Action> actionList = new List<NPC_Action>();

        public virtual void OnStateEnter<T>(StateType stateType , ActionType actionType , T data)
        {
            type = stateType;
            AddAction<T>(actionType,data);
            OnStateEnabled();
        }

        public virtual void OnStateEnabled()
        {
           
        }

        private NPC_Action CreateAction<T>(ActionType actionType , T data)
        {
            switch (actionType)
            {
                case ActionType.Idle:
                    NPC_Action IdleAction = new Action_Idle();
                    IdleAction.Initialize<T>(data);
                    actionList.Add(IdleAction);
                    return IdleAction;

                case ActionType.Walk:
                    NPC_Action WalkAction = new Action_Walk();
                    WalkAction.Initialize<T>(data);
                    actionList.Add(WalkAction);
                    return WalkAction;

                default:
                    throw new ArgumentException("Action Not Implemented Yet!");
            }      
        }
        
        public virtual void AddAction<T>(ActionType actionType, T data)
        {
            NPC_Action action = CreateAction<T>(actionType, data);

            if (action== null)
                throw new ArgumentNullException("Action is Null");

            if(!actionList.Contains(action))
                actionList.Add(action);
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
    }
}