using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LoopingEntity
{
	public float RunningSpeed = 10;
	public float JumpForce = 500;

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
