using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Entities.View
{
    public class TransformViewInstaller : EcsViewInstaller
    {
        [SerializeField] private Transform _transform;

        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<TransformView>().Add(entity).Value = transform;
        }
    }
}