using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Entities.Core
{
    public sealed class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>> _filter;

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            foreach (int entity in _filter.Value)
            {
                ref MoveDirection direction = ref _filter.Pools.Inc1.Get(entity);
                ref MoveSpeed speed = ref _filter.Pools.Inc2.Get(entity);
                ref Position position = ref _filter.Pools.Inc3.Get(entity);

                position.Value += direction.Value * (speed.Value * deltaTime);
            }
        }
    }
}