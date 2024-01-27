using LaughGame;
using LaughGame.Assets.Scripts.Model.Abilities;
using LaughGame.Model.Abilities;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IMovable
{
    public float horizontal;
    public float vertical;
    public float speed = 1.0f;

    Vector2 moveDirection;

    Rigidbody2D rb;
    StateManager states;
    Transform trans;

    [SerializeField]
    private Camera _camera;

    public Transform MovableTransform => trans;

    public bool SelfMovementEnabled { get => states.canMove; set => states.canMove = value; }

    public Vector2 FacingDirection { get; private set; }


    private Vector2 _facingDirection;

    private IAbilitiesEntitiesProvider _abilitiesEntitiesProvider;
    
    [Inject]
    public void Consruct(IAbilitiesEntitiesProvider abilitiesEntitiesProvider)
    {
        _abilitiesEntitiesProvider = abilitiesEntitiesProvider;
        _abilitiesEntitiesProvider.SetMovable(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        states = GetComponent<StateManager>();
        trans = transform;
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

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, float.PositiveInfinity))
        {
            FacingDirection = (hit.point - trans.position).normalized;
        }
        
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
        Move(new Vector2(moveDirection.x * speed, moveDirection.y * speed));
    }

    public void Move(Vector2 movementVelocity)
    {
        rb.velocity = movementVelocity;
    }

    private void OnDestroy()
    {
        _abilitiesEntitiesProvider.SetMovable(null);
    }
}
