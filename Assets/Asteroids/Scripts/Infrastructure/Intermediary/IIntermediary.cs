using System;

namespace Asteroids.Infrastructure.Intermediary
{
    public interface IIntermediary<TStates> where TStates : Enum
    {
        event Action<TStates> OnStateChanged;
        event Action<TStates> OnEndState;
        void InitIntermediaryStates();
        void DisableIntermediaryStates();
    }
}