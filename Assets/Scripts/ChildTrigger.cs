using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
	public string MethodName = "";

	private void OnTriggerEnter2D(Collider2D other) {
		transform.SendMessageUpwards(MethodName, other);
	}
}
