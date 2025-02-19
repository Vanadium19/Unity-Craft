using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace Game.Context.Player
{
    public class PlayerMoveController : IEcsRunSystem
    {
        private EcsSharedInject<GameData> _gameData;
        private EcsFilterInject<Inc<MoveDirection>> _filter;

        public void Run(IEcsSystems systems)
        {
            ref var direction = ref _gameData.Value.InputData.MoveDirection;

            foreach (int entity in _filter.Value)
                _filter.Pools.Inc1.Get(entity).Value = direction;
        }
    }
}