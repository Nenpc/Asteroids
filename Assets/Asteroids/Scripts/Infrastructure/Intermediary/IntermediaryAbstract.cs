using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.Infrastructure.Intermediary
{
    public abstract class IntermediaryAbstract<TState> : IIntermediary<TState> where TState : Enum
    {
        public event Action<TState> OnStateChanged;
        public event Action<TState> OnEndState;

        protected abstract TState FirstState { get; }
        protected abstract IEnumerable<IIntermediaryState<TState>> States { get; }
        
        protected IIntermediaryState<TState> _activeState;
        protected bool _active;

        public void InitIntermediaryStates()
        {
            _active = true;
            StartState(FirstState);
        }

        public void DisableIntermediaryStates()
        {
            _active = false;
            _activeState?.End();
        }

        private void StartState(TState stateType)
        {
            if (!_active) return;

            var state = States.FirstOrDefault(x => x.State.Equals(stateType));

            if (state == default)
            {
                Debug.LogWarning($"Logic for state {stateType} doesn't exist!");
                return;
            }
            OnStateChanged?.Invoke(stateType);
            _activeState = state;
            _activeState.ChangeStateAction += ChangeState;
            state.Start();
        }

        private void ChangeState(TState nextState)
        {
            if (!_active) return;
            
            if (_activeState != null)
            {
                _activeState.ChangeStateAction -= ChangeState;
                _activeState.End();
                OnEndState?.Invoke(_activeState.State);
            }
            
            StartState(nextState);
        }
    }
}