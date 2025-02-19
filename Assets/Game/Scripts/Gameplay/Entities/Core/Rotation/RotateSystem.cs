using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Entities.Core
{
    public sealed class RotateSystem : IEcsRunSystem
    {
        private readonly float3 Up = new(0, 1, 0);

        private readonly EcsFilterInject<Inc<RotateDirection, RotationSpeed, Rotation>> _filter;

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            foreach (int entity in _filter.Value)
            {
                ref RotateDirection direction = ref _filter.Pools.Inc1.Get(entity);
                ref RotationSpeed speed = ref _filter.Pools.Inc2.Get(entity);
                ref Rotation rotation = ref _filter.Pools.Inc3.Get(entity);

                if (math.all(direction.Value == float3.zero))
                    return;

                quaternion target = quaternion.LookRotation(direction.Value, Up);
                rotation.Value = math.slerp(rotation.Value, target, speed.Value * deltaTime);
            }
        }
    }
}