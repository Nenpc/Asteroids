using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Scripts.Infrastructure.Pool
{
    public abstract class PoolAbstract<T> where T : new()
    {
        public int initialSize = 10;
        private List<T> _pool = new List<T>();

        void Initialize()
        {
            for (int i = 0; i < initialSize; i++)
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