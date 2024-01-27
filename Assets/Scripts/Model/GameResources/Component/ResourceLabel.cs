using System;
using AsyncReactAwait.Bindable;
using LaughGame.GameResources;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace LaughGame.Model.GameResources.Component
{
    public class ResourceLabel : MonoBehaviour
    {

        [SerializeField]
        private string _resourceId;

        private IBindable<float> _resource;

        [Inject]
        public void Construct(IResourcesModel resourcesModel)
        {
            _resource = resourcesModel.GetResource(_resourceId);
            _resource.Bind(OnResChanged);
        }

        private void OnResChanged(float obj)
        {
            
        }

        private void OnDestroy()
        {            
            _resource.Unbind(OnResChanged);
        }
    }
}