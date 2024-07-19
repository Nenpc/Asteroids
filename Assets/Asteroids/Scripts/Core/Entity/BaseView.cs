using UnityEngine;

namespace Asteroids.Scripts.Core.Entity
{
    public abstract class BaseView : MonoBehaviour
    {
        private BaseModel _model;
        public BaseModel Model => _model;

        public void Init(BaseModel model)
        {
            _model = model;
        }
    }
}