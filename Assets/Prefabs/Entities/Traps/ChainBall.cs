using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBall : LoopingEntity
{
	public float RotationSpeed = 1;
	void FixedUpdate() {
		_rb.rotation += RotationSpeed * Time.deltaTime;
	}
}
