using Game.Common;
using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Entities.Content.Player
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsPrototype _bulletPrefab;

        private readonly EcsFilterInject<Inc<Position, Rotation, UnitFireRequired>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorldName.EventWorld;
        private readonly EcsPoolInject<BulletSpawnRequest> _spawnerRequests = EcsWorldName.EventWorld;

        public PlayerAttackSystem(EcsPrototype bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref var fireRequired = ref _filter.Pools.Inc3.Get(entity);

                if (!fireRequired.Value)
                    continue;

                int spawnRequest = _eventWorld.Value.NewEntity();

                _spawnerRequests.Value.Add(spawnRequest) = new BulletSpawnRequest
                {
                    Position = _filter.Pools.Inc1.Get(entity).Value,
                    Rotation = _filter.Pools.Inc2.Get(entity).Value,
                    Prefab = _bulletPrefab,
                };
            }
        }
    }
}