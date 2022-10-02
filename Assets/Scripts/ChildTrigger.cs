using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
	public string MethodEnterName = "";
	public string MethodExitName = "";

	private void OnTriggerEnter2D(Collider2D other) {
		if (MethodEnterName != "")
			transform.SendMessageUpwards(MethodEnterName, other);
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (MethodExitName != "")
			transform.SendMessageUpwards(MethodExitName, other);
	}
}
