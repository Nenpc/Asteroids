using System;
using Asteroids.Scripts.Core.Hero.Configs;
using Asteroids.Scripts.Core.Hero.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Core.Hero.Models
{
    public sealed class HeroModel : IHeroModel
    {
        private const float BoundStepOver = 0.5f;
        
        public event Action OnDestroy;

        private readonly IHeroConfig _heroConfig;
        private readonly UserInput _userInput;

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

        public HeroModel(IHeroConfig heroConfig)
        {
            _heroConfig = heroConfig;
            _userInput = new UserInput();

            _maxSpeed = _heroConfig.MaxSpeed;
            _rotationSpeed = _heroConfig.RotationSpeed;
            _acceleration = _heroConfig.Acceleration;
            _braking = _heroConfig.Braking;
            _currentSpeed = Vector2.zero;
            _angle = 0;
        }

        public void Start()
        {
            CalculateWorldBounds();
            
            _view = GameObject.Instantiate(_heroConfig.HeroView);
            
            _moveInputAction = _userInput.Player.Move;
            _userInput.Player.Move.Enable();
            
            _rotationInputAction = _userInput.Player.Rotation;
            _userInput.Player.Rotation.Enable();
            
            _userInput.Player.FireBullet.performed += FireBullet;
            _userInput.Player.FireBullet.Enable();
            
            _userInput.Player.FireLaser.performed += FireLaser;
            _userInput.Player.FireLaser.Enable();
            
            _userInput.Player.Pause.performed += Pause;
            _userInput.Player.Pause.Enable();
        }

        private void CalculateWorldBounds()
        {
            float width = Camera.main.pixelWidth;
            float height = Camera.main.pixelHeight;

            _bottomRight = Camera.main.ScreenToWorldPoint(new Vector2 (width, 0));
            _topLeft = Camera.main.ScreenToWorldPoint(new Vector2 (0, height));
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
                _currentSpeed = Vector2.ClampMagnitude((Vector2.up * _acceleration * Time.fixedTime) + _currentSpeed, _maxSpeed);
            }

            if (_currentSpeed != Vector2.zero)
            {
                _view.transform.Translate(_currentSpeed, Space.Self);
                _currentSpeed = Vector2.Lerp(_currentSpeed, Vector2.zero, _braking);
            }

            CheckBounds();
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

        public void Destroy()
        {
            GameObject.Destroy(_view);
            _currentSpeed = Vector2.zero;
            _angle = 0;
        }

        private void FireBullet(InputAction.CallbackContext context)
        {

        }

        private void FireLaser(InputAction.CallbackContext context)
        {
            Debug.Log("FireLaser");
        }

        private void Pause(InputAction.CallbackContext context)
        {
            Debug.Log("Pause");
        }
    }
}