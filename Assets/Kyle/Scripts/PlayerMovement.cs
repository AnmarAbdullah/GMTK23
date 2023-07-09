using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    BoxCollider2D collider2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 10;
    [SerializeField] float walkAcceleration = 5;
    [SerializeField] float groundDeceleration = 2;


    [SerializeField] private Transform myTransform;
    //[SerializeField] private Transform Tyler;

    PlayerInput pInput;

    Vector2 movementInput = Vector2.zero;



    // Start is called before the first frame update
    void Awake()
    {
        //collider2D = transform.GetComponent<BoxCollider2D>();
        //spriteRenderer = transform.GetComponent<SpriteRenderer>();
        //animator = transform.GetComponent<Animator>();
        pInput = new PlayerInput();
    }

    void OnEnable()
    {
        pInput.Enable();
        pInput.Player.Movement.performed += OnMove;
        pInput.Player.Movement.canceled += OnMoveCancel;
    }

    void OnDisable()
    {
        pInput.Disable();
        pInput.Player.Movement.performed -= OnMove;
        pInput.Player.Movement.canceled -= OnMoveCancel;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnMoveCancel(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    void Update()
    {
        string[] ee = Input.GetJoystickNames();
        for (int i = 0; i < ee.Length; i++)
        {
            print(ee[i]);  
        }

        Movement(myTransform);
    }

    void Movement(Transform player)
    {
        //float moveInput = Input.GetAxisRaw(horizontal);
        //float moveInputVertical = Input.GetAxisRaw(vertical);

        Animator animator = player.GetComponent<Animator>();
        var collider2D = player.GetComponent<BoxCollider2D>();
        var spriteRenderer = player.GetComponent<SpriteRenderer>();

        if (movementInput.x != 0)
        {
            //Debug.Log(moveInput);
            velocity.x = Mathf.MoveTowards(velocity.x, speed * movementInput.x, walkAcceleration * Time.deltaTime);
            animator.SetBool("IsWalkingH", true);
            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
            animator.SetBool("IsWalkingH", false);
        }


        if (movementInput.y != 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, speed * movementInput.y, walkAcceleration * Time.deltaTime);

            if (movementInput.y > 0)
            {
                animator.SetBool("IsWalkingV", true);
            }
        }
        else
        {
            velocity.y = Mathf.MoveTowards(velocity.y, 0, groundDeceleration * Time.deltaTime);
            animator.SetBool("IsWalkingV", false);
        }

        player.Translate(velocity * Time.deltaTime);

        Collider2D[] hits = Physics2D.OverlapBoxAll(player.position, collider2D.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == collider2D)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(collider2D);

            if (colliderDistance.isOverlapped)
            {
                player.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }
    }
}
