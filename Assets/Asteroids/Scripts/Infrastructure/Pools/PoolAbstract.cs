using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Scripts.Infrastructure.Pools
{
    public abstract class PoolAbstract<T> where T : new()
    {
        private int _initialSize;
        private List<T> _pool = new List<T>();

        void Initialize(int initialSize = 10)
        {
            _initialSize = initialSize;
            for (int i = 0; i < _initialSize; i++)
            {
                _pool.Add(new T());
            }
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                var result = _pool.Last();
                _pool.Remove(result);
                return result;
            }
            else
            {
                return new T();
            }
        }
        
        public void Return(T obj)
        {
            _pool.Add(obj);
        }
    }
}