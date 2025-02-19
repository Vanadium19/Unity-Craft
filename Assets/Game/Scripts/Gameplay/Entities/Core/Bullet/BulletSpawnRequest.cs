using System;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace Game.Entities.Core
{
    [Serializable]
    public struct BulletSpawnRequest
    {
        public float3 Position;
        public quaternion Rotation;
        public EcsPrototype Prefab;
    }
}