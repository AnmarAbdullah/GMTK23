using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 velocity;
    BoxCollider2D collider2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 10;
    [SerializeField] float walkAcceleration = 5;
    [SerializeField] float groundDeceleration = 2;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = transform.GetComponent<BoxCollider2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float moveInputVertical = Input.GetAxisRaw("Vertical");

        if (moveInput != 0)
        {
            Debug.Log(moveInput);
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
            animator.SetBool("IsWalkingH", true);
            if (moveInput > 0)
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


        if (moveInputVertical != 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputVertical, walkAcceleration * Time.deltaTime);

            if(moveInputVertical > 0)
            {
                animator.SetBool("IsWalkingV", true);
            }
        }
        else
        {
            velocity.y = Mathf.MoveTowards(velocity.y, 0, groundDeceleration * Time.deltaTime);
            animator.SetBool("IsWalkingV", false);
        }

        transform.Translate(velocity * Time.deltaTime);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, collider2D.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == collider2D)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(collider2D);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }
    }
}
