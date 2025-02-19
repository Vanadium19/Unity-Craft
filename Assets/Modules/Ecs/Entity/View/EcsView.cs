using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite
{
    [DisallowMultipleComponent]
    public class EcsView : MonoBehaviour
    {
        public virtual string Name => this.name; 
        
        [SerializeField]
        private List<EcsViewInstaller> _installers;

        public int Entity => _entity;
        public EcsWorld World => _world;

        private bool _shown;
        private int _entity;
        private EcsWorld _world;

        public void Show(EcsWorld world, int entity)
        {
            if (_shown)
            {
                Debug.LogError($"EcsView {this.name} is already shown!", this);
                return;
            }

            _entity = entity;
            _world = world;

            for (int i = 0, count = _installers.Count; i < count; i++)
            {
                EcsViewInstaller installer = _installers[i];
                if (installer != null)
                    installer.Install(in world, in entity);
            }
            
            this.gameObject.SetActive(true);
            _shown = true;
        }

        public void Hide()
        {
            if (!_shown)
            {
                Debug.LogError($"EntityView {this.name} is already hidden!", this);
                return;
            }
            
            this.gameObject.SetActive(false);

            _world = null;
            _entity = -1;
            _shown = false;
        }
    }
}