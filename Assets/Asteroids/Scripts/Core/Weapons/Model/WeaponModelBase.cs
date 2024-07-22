using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public abstract class WeaponModelBase : IBaseModel
    {
        public event Action<IBaseModel> OnDestroy;
        
        private readonly WeaponView _view;
        private readonly IShooter _owner;
        protected readonly IUpdateProvider _updateProvider;

        public Enums.Models ModelType => Models.Weapon;
        public abstract Enums.Weapons WeaponType { get; }
        public IShooter Owner => _owner;
        public BaseView View => _view;

        public WeaponModelBase(WeaponView weaponView, IShooter owner, IUpdateProvider updateProvider)
        {
            _owner = owner;
            _view = weaponView;
            _updateProvider = updateProvider;
            _view.Init(this);
            _view.OnTrigger += OnTriggerEnter;
            _updateProvider.OnFixedUpdate += FixedUpdate;
        }

        protected abstract void OnTriggerEnter(Collider2D obj);

        public abstract void FixedUpdate();

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            _view.OnTrigger -= OnTriggerEnter;
            _updateProvider.OnFixedUpdate -= FixedUpdate;
        }
    }
}