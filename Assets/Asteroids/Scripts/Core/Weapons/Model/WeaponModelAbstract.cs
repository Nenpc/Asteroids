using System;
using Asteroids.Scripts.Core.Entity;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public abstract class WeaponModelAbstract : BaseModel
    {
        public event Action<BaseModel> OnDestroy;
        
        private readonly WeaponView _view;
        private readonly BaseModel _owner;
        protected readonly IUpdateProvider _updateProvider;

        public Enums.Models ModelType => Models.Weapon;
        public Enums.Weapons WeaponType { get; }
        public BaseModel Owner => _owner;
        public WeaponView View => _view;

        public WeaponModelAbstract(WeaponView weaponView, BaseModel owner, IUpdateProvider updateProvider)
        {
            _owner = owner;
            _view = weaponView;
            _updateProvider = updateProvider;
            _view.Init(this);
            _view.OnTrigger += OnTriggerEnter;
            _updateProvider.OnFixedUpdate += FixedUpdate;
        }

        protected abstract void OnTriggerEnter(Collider obj);

        public abstract void FixedUpdate();

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            _view.OnTrigger -= OnTriggerEnter;
            _updateProvider.OnFixedUpdate -= FixedUpdate;
        }
    }
}