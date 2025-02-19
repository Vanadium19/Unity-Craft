using Game.Entities.Core;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Entities.Content.Projectiles
{
    [CreateAssetMenu(
        fileName = "Projectile",
        menuName = "SampleGame/Entities/New Projectile"
    )]
    public class ProjectilePrototype : EcsPrototype
    {
        protected override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<Position>().Get(entity).Value = float3.zero;
        }
    }
}