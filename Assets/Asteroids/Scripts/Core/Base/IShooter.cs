using UnityEngine;

namespace Asteroids.Scripts.Core.Base
{
    public interface IShooter
    {
        BaseView View { get; }
        Transform BulletStartPosition { get; }
    }
}