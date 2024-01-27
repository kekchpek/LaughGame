using System.Collections.Generic;
using UnityEngine;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public class StarsView : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> _stars = new();
        
        public void SetStars(int starsCount)
        {
            for (int i = 0; i < _stars.Count; i++)
            {
                _stars[i].SetActive(i < starsCount);
            }
        }
    }
}