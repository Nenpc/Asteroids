using System;

namespace Asteroids.Scripts.Infrastructure.UpdateProvider
{
    public interface IUpdateProvider
    {
        event Action OnUpdate;
        event Action OnLateUpdate;
        event Action OnFixedUpdate;
    }
}