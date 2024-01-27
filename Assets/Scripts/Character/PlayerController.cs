using LaughGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float speed = 1.0f;

    Vector2 moveDirection;
    
    Rigidbody2D rb;
    StateManager states;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        states = GetComponent<StateManager>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (horizontal > 0 && !states.lookRight) { states.lookRight = true; }
        else if (horizontal < 0 && states.lookRight) { states.lookRight = false; }

        if (vertical > 0 && !states.lookUp) { states.lookUp = true; }
        else if (vertical < 0 && !states.lookDown) { states.lookDown = true; }
        else if (vertical == 0) { states.lookUp = false; states.lookDown = false; }

    }

    private void FixedUpdate()
    {
        if (states.canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
}
