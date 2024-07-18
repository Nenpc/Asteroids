using System;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class UFOEnemy : BaseEnemy
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