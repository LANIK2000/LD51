using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LoopingEntity
{
	public float RunningSpeed = 15;
	public float JumpForce = 15;

	SpriteRenderer _spriteRenderer;
	public float CoyoteTime = .25f;
	float _coyoteTime = 0;
	bool _onGround = false;
	BoxCollider2D _groundTrigger;
	private static readonly int Speed = Animator.StringToHash("Speed");
	private Animator _animator;

	protected override void Start() {
		_animator = GetComponent<Animator>();
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_groundTrigger = transform.Find("GroundTrigger").GetComponent<BoxCollider2D>();
	}

	void Update() {
		if (!_onGround && _coyoteTime > 0)
			_coyoteTime -= Time.deltaTime;

		if (Input.GetButtonDown("Jump") && _coyoteTime > 0) {
			_rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
			_coyoteTime = 0;
			_onGround = false;
		}

		_animator.SetFloat(Speed, Math.Abs(_rb.velocity.x));
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

	public void onGroundTriggerEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround = true;
				_coyoteTime = CoyoteTime;
				break;
			default:
				break;
		}
	}

	public void onGroundTriggerExit(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround = false;
				break;
			default:
				break;
		}
	}
}
