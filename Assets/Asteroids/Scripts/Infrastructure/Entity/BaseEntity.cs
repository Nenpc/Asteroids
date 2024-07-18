using System;

namespace Asteroids.Scripts.Infrastructure.Entity
{
    public interface BaseEntity
    {
        event Action OnDestroy; 
        void FixedUpdate();
        void Destroy();
    }
}