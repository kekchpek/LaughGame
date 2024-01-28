using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaughGame
{
    public class StateManager : MonoBehaviour
    {
        public int hp = 100;

        public bool lookRight;
        public bool lookUp;
        public bool lookDown;
        public bool canMove;
        public bool isHit;

        public bool ability1;
        public bool ability2;
        public bool ability3;
        public bool ability4;

        private Transform _transform;

        [SerializeField] SpriteRenderer sRenderer;


        // Start is called before the first frame update
        void Start()
        {
            //rb = GetComponent<Rigidbody2D>();
            _transform = transform;
            Initilize();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            if (lookRight && _transform.localScale.x > 0f)
            {
                _transform.localScale = Vector3.Scale(_transform.localScale, new Vector3(-1f, 1f, 1f));
            }
            if (!lookRight && _transform.localScale.x < 0f)
            {
                _transform.localScale = Vector3.Scale(_transform.localScale, new Vector3(-1f, 1f, 1f));
            }
        }

        public void Initilize()
        {
            lookRight = true;
            canMove = true;
        }
    }
}
