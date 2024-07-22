using System;

namespace Asteroids.Scripts.Infrastructure.UpdateProvider
{
    public interface IUpdateProvider
    {
        void Start();
        void Stop();
        bool Pause { get; }

        event Action OnUpdate;
        event Action OnLateUpdate;
        event Action OnFixedUpdate;
    }
}