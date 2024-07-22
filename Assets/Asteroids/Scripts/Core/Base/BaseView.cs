using System;
using UnityEngine;

namespace Asteroids.Scripts.Core.Base
{
    public abstract class BaseView : MonoBehaviour
    {
        private IBaseModel _model;
        public IBaseModel Model => _model;
        
        public event Action<Collider2D> OnTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTrigger?.Invoke(other);
        }

        public void Init(IBaseModel model)
        {
            _model = model;
        }
    }
}