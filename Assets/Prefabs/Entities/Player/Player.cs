using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LoopingEntity
{
	public float RunningSpeed = 15;
	public float JumpForce = 15;

	SpriteRenderer _spriteRenderer;
	float _coyoteTime = 0;
	BoxCollider2D _groundTrigger;
	protected override void Start() {
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_groundTrigger = transform.Find("GroundTrigger").GetComponent<BoxCollider2D>();
	}

	void Update() {
		if (Input.GetButtonDown("Jump"))
			_rb.velocity = new Vector2(
				_rb.velocity.x,
				JumpForce
			);
	}

	void FixedUpdate() {

		float speed = Input.GetAxis("Horizontal") * RunningSpeed;

		if (speed > 0)
			_spriteRenderer.flipX = false;
		else if (speed < 0)
			_spriteRenderer.flipX = true;

		_rb.velocity = new Vector2(
			speed,
			_rb.velocity.y
		);
	}

	public void onGroundTrigger(Collider2D other) {
		
	}
}
