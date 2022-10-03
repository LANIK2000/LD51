using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
			SceneManager.LoadScene(0);
	}
}
