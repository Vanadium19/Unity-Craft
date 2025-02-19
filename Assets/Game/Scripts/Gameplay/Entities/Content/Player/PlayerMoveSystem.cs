using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Entities.Content.Player
{
    public sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitDirection, MoveDirection, RotateDirection>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref UnitDirection unitDirection = ref _filter.Pools.Inc1.Get(entity);
                ref MoveDirection moveDirection = ref _filter.Pools.Inc2.Get(entity);
                ref RotateDirection rotateDirection = ref _filter.Pools.Inc3.Get(entity);

                moveDirection.Value = unitDirection.Value;
                rotateDirection.Value = unitDirection.Value;
            }
        }
    }
}