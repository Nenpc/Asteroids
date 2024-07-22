using Asteroids.Scripts.Core.Base;
using UnityEngine;

namespace Asteroids.Scripts.Core.Hero.Models
{
    public interface IHeroModel : IBaseModel
    {
        void Start();
        float Speed { get; }
    }
}