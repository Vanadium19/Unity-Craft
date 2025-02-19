using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Entities.View
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, Rotation, TransformView>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref Position position = ref _filter.Pools.Inc1.Get(entity);
                ref Rotation rotation = ref _filter.Pools.Inc2.Get(entity);
                ref TransformView view = ref _filter.Pools.Inc3.Get(entity);

                // Debug.Log($"Transform Rotation: {rotation.Value}");
                
                view.Value.SetPositionAndRotation(position.Value, rotation.Value);
            }
        }
    }
}