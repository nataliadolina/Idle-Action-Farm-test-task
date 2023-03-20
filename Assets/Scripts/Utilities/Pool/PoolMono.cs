using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI;
using UI.Animations;

namespace Utilities.Pool
{
    internal class PoolMono<T> where T: MonoBehaviour
    {
        internal T Prefab { get; private set; }
        internal bool autoExpend { get; set; }
        internal Transform Container { get; private set; }
        private List<T> _pool;

        private bool _autoExpand;
        internal PoolMono(T prefab, int count)
        {
            Prefab = prefab;
            Container = null;
        }

        internal PoolMono(T prefab, int count, Transform container, bool autoExpand=true)
        {
            Prefab = prefab;
            Container = container;
            _autoExpand = autoExpand;

            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault=false)
        {
            var createdObject = ContainerRef.Container.InstantiatePrefab(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            T poolComponent = createdObject.GetComponent<T>();
            _pool.Add(poolComponent);
            return poolComponent;
        }

        internal bool HasFreeElement(out T element)
        {
            foreach (var poolObject in _pool)
            {
                if (!poolObject.gameObject.activeInHierarchy)
                {
                    element = poolObject;
                    return true;
                }
            }

            element = null;
            return false;
        }

        internal T GetFreeELement()
        {
            if (HasFreeElement(out T element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (_autoExpand)
            {
                return CreateObject(true);
            }

            throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }
}
