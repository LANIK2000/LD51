using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LoopingEntity
{
	public float RunningSpeed = 15;
	public float JumpForce = 15;

	public override void Start() {
		base.Start();
	}

	void Update() {
		if (Input.GetButtonDown("Jump"))
			_rb.velocity = new Vector2(
				_rb.velocity.x,
				JumpForce
			);
	}

	void FixedUpdate() {
		_rb.velocity = new Vector2(
			Input.GetAxis("Horizontal") * RunningSpeed,
			_rb.velocity.y
		);
	}
}
