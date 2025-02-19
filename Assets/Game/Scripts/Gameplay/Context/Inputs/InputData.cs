using System;
using Unity.Mathematics;

namespace Game.Context.Inputs
{
    [Serializable]
    public struct InputData
    {
        public float3 MoveDirection;
        public bool IsFire;
    }
}