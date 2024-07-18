using System;
using UnityEngine;

namespace Asteroids.Scripts.Infrastructure.UpdateProvider
{
    public sealed class UpdateProvider : MonoBehaviour, IUpdateProvider
    {
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}