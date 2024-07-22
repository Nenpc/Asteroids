using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public interface ICreateAsteroidPosition
    {
        void CreateOnPosition(Transform transform, int amount);
    }
}