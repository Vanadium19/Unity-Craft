using UnityEngine;

namespace Leopotam.EcsLite
{
    public sealed class EcsBaker : MonoBehaviour
    {
        [SerializeField]
        private EcsPrototypeCatalog _prototypes;

        public void BakeScene(EcsWorld world, bool includeInactive = false)
        {
            EcsView[] views = FindObjectsOfType<EcsView>(includeInactive);
         
            for (int i = 0, count = views.Length; i < count; i++)
            {
                EcsView view = views[i];
                this.BakeEntity(world, view);
            }
        }

        public void BakeEntity(in EcsWorld world, in EcsView view)
        {
            string prototypeName = view.Name;
            EcsPrototype prefab = _prototypes.GetPrototype(prototypeName);

            int entity = prefab.Create(world);
            
            IEcsEntityInstaller[] converters = view.GetComponentsInChildren<IEcsEntityInstaller>();
            for (int i = 0, count = converters.Length; i < count; i++)
            {
                IEcsEntityInstaller converter = converters[i];
                converter.Install(world, entity);
            }

            Destroy(view.gameObject);
        }
    }
}