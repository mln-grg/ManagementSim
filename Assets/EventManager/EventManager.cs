using System;

namespace CustomGameEvents
{
    public static class GameEvents
    {
        public static readonly GameEvent LunchTime = new GameEvent();
        public static readonly GameEvent TimeToGetUp = new GameEvent();
        public static readonly GameEvent TimeToSleep = new GameEvent();
    }

    public class GameEvent
    {
        private event Action gameEvent = delegate { };

        public void Invoke()
        {
            gameEvent.Invoke();
        }

        public void RegisterListener(Action listener)
        {
            gameEvent += listener;
        }

        public void UnregisterListener(Action listener)
        {
            gameEvent -= listener;
        }
    }
}