using System;
using Asteroids.Scripts.Core.Entity;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.View
{
    public sealed class WeaponView : BaseView
    {
        public event Action<Collider> OnTrigger;

        private void OnTriggerEnter(Collider other)
        {
            OnTrigger?.Invoke(other);
        }
    }
}