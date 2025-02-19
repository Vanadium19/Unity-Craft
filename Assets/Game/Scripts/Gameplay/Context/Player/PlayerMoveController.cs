using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace Game.Context.Player
{
    public class PlayerMoveController : IEcsRunSystem
    {
        private readonly EcsSharedInject<GameData> _gameData;
        private readonly EcsFilterInject<Inc<UnitDirection>> _filter;

        public void Run(IEcsSystems systems)
        {
            ref var direction = ref _gameData.Value.InputData.MoveDirection;

            foreach (int entity in _filter.Value)
                _filter.Pools.Inc1.Get(entity).Value = direction;
        }
    }
}