using System;

namespace Asteroids.Scripts.Infrastructure.Entity
{
    public abstract class BaseEntity
    {
        public event Action OnDestroy; 
        public abstract void Update();
        public abstract void Destroy();
    }
}