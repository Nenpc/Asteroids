using System;

namespace Asteroids.Infrastructure.Intermediary
{
    public interface IIntermediaryState<TState> where TState : Enum
    {
        event Action<TState> ChangeStateAction;
        TState State { get; }
        void Start();
        void End();
    }
}