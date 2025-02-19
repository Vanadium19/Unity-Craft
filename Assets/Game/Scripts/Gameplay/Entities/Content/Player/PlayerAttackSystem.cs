using Game.Entities.Core;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Entities.Content.Player
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<UnitFireRequired>> _filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref var fireRequired = ref _filter.Pools.Inc1.Get(entity);

                if (fireRequired.Value)
                    Debug.Log("Стрельнул");
            }
        }
    }
}