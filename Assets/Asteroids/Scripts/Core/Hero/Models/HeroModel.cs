using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Hero.Configs;
using Asteroids.Scripts.Core.Hero.View;
using Asteroids.Scripts.Core.Weapons.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Core.Hero.Models
{
    public sealed class HeroModel : IHeroModel, IShooter
    {
        public event Action<IBaseModel> OnDestroy;
        
        private const float BoundStepOver = 0.5f;

        private readonly IHeroConfig _heroConfig;
        private readonly IWeaponsCreator _weaponCreator;
        private readonly UserInput _userInput;

        public Enums.Models ModelType => Enums.Models.Hero;
        public BaseView View => _view;
        public Transform BulletStartPosition => _view.BulletPosition;
        public float Speed => _currentSpeed.sqrMagnitude * 1000;

        private HeroView _view;
        private float _maxSpeed;
        private float _rotationSpeed;
        private float _acceleration;
        private float _braking;

        private Vector2 _currentSpeed;
        private float _angle;
        private InputAction _moveInputAction;
        private InputAction _rotationInputAction;
        
        private Vector2 _bottomRight;
        private Vector2 _topLeft;

        public HeroModel(IHeroConfig heroConfig, IWeaponsCreator weaponCreator, UserInput userInput)
        {
            _heroConfig = heroConfig;
            _weaponCreator = weaponCreator;
            _userInput = userInput;

            _maxSpeed = _heroConfig.MaxSpeed;
            _rotationSpeed = _heroConfig.RotationSpeed;
            _acceleration = _heroConfig.Acceleration;
            _braking = _heroConfig.Braking;
            _currentSpeed = Vector2.zero;
            _angle = 0;
        }
        
        public void Destroy()
        {
            _view.OnTrigger -= OnTriggerEnter;
            GameObject.Destroy(_view.gameObject);
            _currentSpeed = Vector2.zero;
            _angle = 0;

            _moveInputAction = null;
            _userInput.Player.Move.Disable();
            
            _rotationInputAction = null;
            _userInput.Player.Rotation.Disable();
            
            _userInput.Player.FireBullet.performed -= FireBullet;
            _userInput.Player.FireBullet.Disable();
            
            _userInput.Player.FireLaser.performed -= FireLaser;
            _userInput.Player.FireLaser.Disable();
        }
        
        public void Start()
        {
            CalculateWorldBounds();
            
            _view = GameObject.Instantiate(_heroConfig.HeroView);
            _view.Init(this);
            _view.OnTrigger += OnTriggerEnter;
            
            _moveInputAction = _userInput.Player.Move;
            _userInput.Player.Move.Enable();
            
            _rotationInputAction = _userInput.Player.Rotation;
            _userInput.Player.Rotation.Enable();
            
            _userInput.Player.FireBullet.performed += FireBullet;
            _userInput.Player.FireBullet.Enable();
            
            _userInput.Player.FireLaser.performed += FireLaser;
            _userInput.Player.FireLaser.Enable();
        }
        
        private void OnTriggerEnter(Collider2D obj)
        {
            if (obj.gameObject.TryGetComponent<BaseView>(out var baseView))
            {
                if (baseView.Model.ModelType == Enums.Models.Enemy)
                {
                    OnDestroy?.Invoke(this);
                }
            }
        }

        public void FixedUpdate()
        {
            var rotate = _rotationInputAction.ReadValue<float>();
            if (rotate != 0)
            {
                _view.transform.rotation *= Quaternion.Euler(0, 0, _rotationSpeed * rotate);
            }
            
            var move = _moveInputAction.ReadValue<float>();
            if (move != 0)
            {
                _currentSpeed = Vector2.ClampMagnitude((GetAngle() * _acceleration * Time.fixedDeltaTime) + _currentSpeed, _maxSpeed);
                _view.Fire.SetActive(true);
            }
            else
            {
                _view.Fire.SetActive(false);
            }

            if (_currentSpeed != Vector2.zero)
            {
                _view.transform.Translate(_currentSpeed, Space.World);
                _currentSpeed = Vector2.Lerp(_currentSpeed, Vector2.zero, _braking);
            }

            CheckBounds();
        }

        private Vector2 GetAngle()
        {
            var b = Mathf.Deg2Rad * (360 - _view.transform.rotation.eulerAngles.z);
            var AB = Mathf.Sin(b);
            var OA = Mathf.Cos(b);
            return new Vector2(AB, OA);
        }

        private void CheckBounds()
        {
            var position = _view.transform.position;
            if (_topLeft.y + BoundStepOver < position.y)
                _view.transform.position = new Vector3(_view.transform.position.x, _bottomRight.y);
            else if (_topLeft.x - BoundStepOver > position.x)
                _view.transform.position = new Vector3(_bottomRight.x, _view.transform.position.y);
            else if (_bottomRight.y - BoundStepOver > position.y)
                _view.transform.position = new Vector3(_view.transform.position.x, _topLeft.y);
            else if (_bottomRight.x + BoundStepOver < position.x)
                _view.transform.position = new Vector3(_topLeft.x, _view.transform.position.y);
        }

        private void FireBullet(InputAction.CallbackContext context)
        {
            _weaponCreator.CreateWeapon(Enums.Weapons.Bullet,this);
        }

        private void FireLaser(InputAction.CallbackContext context)
        {
            _weaponCreator.CreateWeapon(Enums.Weapons.Laser,this);
        }
        
        private void CalculateWorldBounds()
        {
            float width = Camera.main.pixelWidth;
            float height = Camera.main.pixelHeight;

            _bottomRight = Camera.main.ScreenToWorldPoint(new Vector2 (width, 0));
            _topLeft = Camera.main.ScreenToWorldPoint(new Vector2 (0, height));
        }
    }
}