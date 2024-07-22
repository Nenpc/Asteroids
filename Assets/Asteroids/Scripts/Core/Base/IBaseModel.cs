using System;
using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Scripts.Core.Base
{
    public interface IBaseModel
    {
        event Action<IBaseModel> OnDestroy;

        Models ModelType { get; }
        BaseView View { get; }
        void FixedUpdate();
        void Destroy();
    }
}