using System;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class AsteroidEnemy : BaseEnemy
    {
        public event Action OnDestroy;

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}