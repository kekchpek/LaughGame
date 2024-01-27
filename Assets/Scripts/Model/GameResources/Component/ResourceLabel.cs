using AsyncReactAwait.Bindable;
using LaughGame.GameResources;
using TMPro;
using UnityEngine;
using Zenject;

namespace LaughGame.Model.GameResources.Component
{
    public class ResourceLabel : MonoBehaviour
    {

        [SerializeField]
        private string _resourceId;

        [SerializeField]
        private TMP_Text _text;

        private IBindable<float> _resource;

        [Inject]
        public void Construct(IResourcesModel resourcesModel)
        {
            _resource = resourcesModel.GetResource(_resourceId);
            _resource.Bind(OnResChanged);
        }

        private void OnResChanged(float val)
        {
            _text.text = ((int)val).ToString();
        }

        private void OnDestroy()
        {            
            _resource.Unbind(OnResChanged);
        }
    }
}