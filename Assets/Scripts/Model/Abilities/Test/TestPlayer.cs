using System.Collections;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities.Test
{
    public class TestPlayer : MonoBehaviour, IMovable
    {
        public Transform MovableTransform => throw new System.NotImplementedException();

        public bool SelfMovementEnabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Vector2 Direction => throw new System.NotImplementedException();



        Vector2 _inputDir;

        public void Move(Vector2 movementVelocity)
        {
            throw new System.NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _inputDir.x = Input.GetAxis("horizontal");
        }
    }
}