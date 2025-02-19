using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite
{
    [CreateAssetMenu(
        fileName = "EcsPrototypeCatalog",
        menuName = "Ecs/New EcsPrototypeCatalog"
    )]
    public sealed class EcsPrototypeCatalog : ScriptableObject
    {
        [SerializeField]
        private List<EcsPrototype> _prefabs;

        public int Count => _prefabs.Count;

        public KeyValuePair<string, EcsPrototype> GetPrototype(int index)
        {
            EcsPrototype prefab = _prefabs[index];
            return new KeyValuePair<string, EcsPrototype>(prefab.Name, prefab);
        }

        public EcsPrototype GetPrototype(string name)
        {
            for (int i = 0, count = _prefabs.Count; i < count; i++)
            {
                EcsPrototype prefab = _prefabs[i];
                string prefabName = prefab.Name;
                if (prefabName == name)
                    return prefab;
            }

            throw new Exception($"Prefab with name {name} is not found!");
        }
    }
}