using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Asteroids.Scripts.Infrastructure.Pools
{
    public sealed class PoolGameObject
    {
        private GameObject _prefab;
        private int _initialSize = 10;
        private List<GameObject> _pool = new List<GameObject>();
        
        public PoolGameObject(GameObject prefab, int initialSize = 10)
        {
            _prefab = prefab;
            _initialSize = initialSize;
            for (int i = 0; i < _initialSize; i++)
            {
                var obj = GameObject.Instantiate(_prefab, Vector3.zero, quaternion.identity);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                var result = _pool.Last();
                _pool.Remove(result);
                return result;
            }
            else
            {
                var obj = GameObject.Instantiate(_prefab, Vector3.zero, quaternion.identity);
                obj.SetActive(false);
                return obj;
            }
        }
        
        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.rotation = quaternion.identity;
            obj.transform.position = Vector3.zero;
            obj.transform.parent = null;
            _pool.Add(obj);
        }
    }
}