using UnityEngine;

namespace Leopotam.EcsLite
{
    public abstract class EcsViewInstaller : MonoBehaviour
    {
        public abstract void Install(in EcsWorld world, in int entity);
    }
}