using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float RotationSpeed = 1;
	void FixedUpdate() {
		transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + RotationSpeed * Time.deltaTime);
	}
}
