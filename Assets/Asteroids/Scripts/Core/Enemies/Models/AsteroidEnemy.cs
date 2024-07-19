using System;
using Asteroids.Scripts.Core.Entity;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class AsteroidEnemy : BaseEnemy
    {
        public event Action<BaseModel> OnDestroy;
        
        public Enums.Models ModelType => Enums.Models.Enemy;

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