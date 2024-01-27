using System.Collections.Generic;
using LaughGame.GameResources;
using UnityEngine;
using UnityEngine.UI;

namespace LaughGame.Interaction.UI
{
    public class PriceView : MonoBehaviour
    {

        [SerializeField]
        private List<Image> _images = new();

        public void SetPrice(IEnumerable<ResourceId?> price)
        {
            var i = 0;
            foreach (var res in price)
            {
                if (!res.HasValue)
                {
                    _images[i].gameObject.SetActive(false);
                }
                else
                {
                    _images[i].gameObject.SetActive(true);
                    _images[i].sprite = Resources.Load<Sprite>($"Smiles/{res.Value}");
                }
                i++;
            }
        }
        
    }
}