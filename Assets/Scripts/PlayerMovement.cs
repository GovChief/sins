using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static int FLOOR_LAYER = 8;

    public float walkSpeed;
    public float sprintSpeed;
    public float jumpSpeed;

    Rigidbody2D rigidBody2D;
    Animator animator;

    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rigidBody2D.position;
        if (rigidBody2D.velocity.x != 0)
        {
            animator.SetInteger("Walk", Mathf.RoundToInt(Mathf.Sign(rigidBody2D.velocity.x)));
        }
        else
        {
            animator.SetInteger("Walk", 0);
        }
        
    }

    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * walkSpeed;
        horizontalMovement += Mathf.Sign(horizontalMovement) * sprintSpeed * (Input.GetButton("Sprint") ? 1 : 0);
        rigidBody2D.velocity = new Vector2(horizontalMovement, rigidBody2D.velocity.y);
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.layer == FLOOR_LAYER)
        {
            isGrounded = true;
            Debug.Log(isGrounded.ToString());
        }
    }
}
