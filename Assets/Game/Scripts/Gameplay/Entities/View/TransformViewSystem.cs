using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Entities.View
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Position, TransformView>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref Position position = ref _filter.Pools.Inc1.Get(entity);
                ref TransformView view = ref _filter.Pools.Inc2.Get(entity);

                view.Value.position = position.Value;
            }
        }
    }
}