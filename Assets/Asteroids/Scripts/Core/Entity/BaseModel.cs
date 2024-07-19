using System;
using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Scripts.Core.Entity
{
    public interface BaseModel
    {
        event Action<BaseModel> OnDestroy;

        Models ModelType { get; }
        void FixedUpdate();
        void Destroy();
    }
}