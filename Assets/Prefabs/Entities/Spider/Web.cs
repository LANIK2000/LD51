using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
	public Spider Spider;
	public bool PlayerInWeb;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
			PlayerInWeb = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player")
			PlayerInWeb = false;
	}
}
