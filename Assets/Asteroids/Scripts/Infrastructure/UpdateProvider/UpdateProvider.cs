using System;
using UnityEngine;

namespace Asteroids.Scripts.Infrastructure.UpdateProvider
{
    public sealed class UpdateProvider : MonoBehaviour, IUpdateProvider
    {
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;

        public void Start() => _pause = false;
        public void Stop() => _pause = true;
        public bool Pause => _pause;
        
        private bool _pause;
        
        private void Update()
        {
            if (!_pause)
                OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            if (!_pause)
                OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            if (!_pause)
            {
                OnFixedUpdate?.Invoke();
            }
        }
    }
}