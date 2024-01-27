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

        SpriteRenderer sRenderer;


        // Start is called before the first frame update
        void Start()
        {
            //rb = GetComponent<Rigidbody2D>();
            sRenderer = GetComponent<SpriteRenderer>();
            Initilize();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            sRenderer.flipX = !lookRight;
        }

        public void Initilize()
        {
            lookRight = true;
            canMove = true;
        }
    }
}
