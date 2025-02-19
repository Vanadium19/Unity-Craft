using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Leopotam.EcsLite
{
    [DisallowMultipleComponent]
    public sealed class EcsViewPool : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private EcsViewCatalog[] _catalogs;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, EcsView> _prefabs = new();

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<string, Queue<EcsView>> _pools = new();

        private void Awake()
        {
            for (int i = 0, count = _catalogs.Length; i < count; i++)
                this.AddPrefabs(_catalogs[i]);
        }

        public EcsView Rent(string name)
        {
            Queue<EcsView> pool = this.GetPool(name);
            if (pool.TryDequeue(out EcsView presenter))
                return presenter;

            if (!_prefabs.TryGetValue(name, out EcsView prefab))
                throw new KeyNotFoundException($"Entity view with name <{name}> was not present in Entity View Pool!");

            return Instantiate(prefab, _container);
        }

        public void Return(EcsView view)
        {
            Queue<EcsView> pool = this.GetPool(view.Name);
            pool.Enqueue(view);
            view.transform.parent = _container;
        }

        public void Clear()
        {
            foreach (Queue<EcsView> pool in _pools.Values)
            {
                foreach (EcsView view in pool)
                    Destroy(view.gameObject);

                pool.Clear();
            }

            _pools.Clear();
        }

        private Queue<EcsView> GetPool(string name)
        {
            if (_pools.TryGetValue(name, out Queue<EcsView> pool))
                return pool;

            pool = new Queue<EcsView>();
            _pools.Add(name, pool);
            return pool;
        }

        public void AddPrefabs(EcsViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, EcsView value) = catalog.GetPrefab(i);
                _prefabs.Add(key, value);
            }
        }

        public void AddPrefab(string entityName, EcsView prefab)
        {
            _prefabs.Add(entityName, prefab);
        }

        public void RemovePrefabs(EcsViewCatalog catalog)
        {
            for (int i = 0, count = catalog.Count; i < count; i++)
            {
                (string key, _) = catalog.GetPrefab(i);
                _prefabs.Remove(key);
            }
        }

        public void RemovePrefab(string entityName)
        {
            _prefabs.Remove(entityName);
        }
    }
}