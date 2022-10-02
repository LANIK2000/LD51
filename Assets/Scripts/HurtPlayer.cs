using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "PlayerAvatar")
			LoopSaveSystem.instance.LoadAll();
	}
}
