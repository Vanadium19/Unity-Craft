using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Context.Player
{
    public class PlayerAttackController : IEcsRunSystem
    {
        private EcsSharedInject<GameData> _gameData;
        private EcsFilterInject<Inc<UnitFireRequired>> _filter;

        public void Run(IEcsSystems systems)
        {
            ref bool isFire = ref _gameData.Value.InputData.IsFire;

            foreach (int entity in _filter.Value)
                _filter.Pools.Inc1.Get(entity).Value = isFire;
        }
    }
}