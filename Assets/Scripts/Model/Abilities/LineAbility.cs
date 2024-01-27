using System.Collections;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LineAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public IMovable AbilityParent { get; private set; }



        public void Execute()
        {
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}