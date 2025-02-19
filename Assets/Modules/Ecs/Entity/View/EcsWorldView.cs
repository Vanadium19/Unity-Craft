using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite
{
    [DisallowMultipleComponent]
    public class EcsWorldView : MonoBehaviour, IEcsWorldEventListener
    {
        private readonly Dictionary<int, EcsView> _activeViews = new();

        [SerializeField]
        private Transform _viewport;

        [SerializeField]
        private EcsViewPool _viewPool;

        private EcsWorld _world;
        private EcsPool<EcsName> _entityNames;

        private readonly Queue<int> _entitiesToSpawn = new();
        private readonly Queue<int> _entitiesToDespawn = new();

        private bool _shown;
        
        public void Show(EcsWorld world)
        {
            if (_shown)
            {
                Debug.LogError($"EcsWorldView {this.name} is already shown!", this);
                return;
            }

            _world = world ?? throw new ArgumentNullException(nameof(world));
            _entityNames = _world.GetPool<EcsName>();
            
            _world.AddEventListener(this);
            
            int[] entities = null;
            int count = _world.GetAllEntities(ref entities);

            for (int i = 0; i < count; i++) 
                this.SpawnView(entities[i]);

            _shown = true;
        }

        public void Hide()
        {
            if (!_shown)
            {
                Debug.LogError($"EcsWorldView {this.name} is already hidden!", this);
                return;
            }

            _world.RemoveEventListener(this);
            
            int[] entities = null;
            int count = _world.GetAllEntities(ref entities);

            for (int i = 0; i < count; i++) 
                this.DespawnView(entities[i]);
            
            _world = null;
            _entityNames = null;
            _shown = false;
        }

        void IEcsWorldEventListener.OnEntityCreated(int entity) => _entitiesToSpawn.Enqueue(entity);

        void IEcsWorldEventListener.OnEntityDestroyed(int entity) => _entitiesToDespawn.Enqueue(entity);

        void IEcsWorldEventListener.OnWorldDestroyed(EcsWorld world) => this.Hide();

        protected virtual string GetEntityName(int entity) => _entityNames.Get(entity).value;
        
        public void FixedUpdate()
        {
            if (!_shown)
                return;

            foreach (int entity in _entitiesToSpawn) 
                this.SpawnView(entity);

            foreach (int entity in _entitiesToDespawn) 
                this.DespawnView(entity);
            
            _entitiesToSpawn.Clear();
            _entitiesToDespawn.Clear();
        }

        private void SpawnView(int entity)
        {
            string name = this.GetEntityName(entity);
            EcsView view = _viewPool.Rent(name);
            view.transform.parent = _viewport;
            view.Show(_world, entity);
            
            _activeViews.Add(entity, view);
        }

        private void DespawnView(int entity)
        {
            if (!_activeViews.Remove(entity, out EcsView view))
                return;

            view.Hide();
            _viewPool.Return(view);
        }

        void IEcsWorldEventListener.OnEntityChanged(int entity, short poolId, bool added)
        {
            //Do nothing...
        }

        void IEcsWorldEventListener.OnFilterCreated(EcsFilter filter)
        {
            //Do nothing...
        }

        void IEcsWorldEventListener.OnWorldResized(int newSize)
        {
            //Do nothing...
        }
    }
}