using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public static int FLOOR_LAYER = 8;

	public float walkSpeed;
	public float sprintSpeed;
	public float jumpSpeed;

	Rigidbody2D rigidBody2D;
	Animator animator;

	private bool isGrounded = true;

	// Start is called before the first frame update
	void Start() {
		rigidBody2D = GetComponentInChildren<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Update() {
		animator.SetInteger("DirectionX", MathSignWithZero(rigidBody2D.velocity.x));
		animator.SetInteger("DirectionY", MathSignWithZero(rigidBody2D.velocity.y));
	}

	private void FixedUpdate() {
		float horizontalMovement = Input.GetAxis("Horizontal") * walkSpeed;
		horizontalMovement += Mathf.Sign(horizontalMovement) * sprintSpeed * (Input.GetButton("Sprint") ? 1 : 0);
		rigidBody2D.velocity = new Vector2(horizontalMovement, rigidBody2D.velocity.y);
		if (Input.GetButton("Jump") && isGrounded) {
			rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
			isGrounded = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		isGrounded |= collision.gameObject.layer == FLOOR_LAYER;
	}

	private int MathSignWithZero(float number) {
		if (number == 0) {
			return 0;
		}
		return Mathf.RoundToInt(Mathf.Sign(number));
	}
}
