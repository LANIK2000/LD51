using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparent : MonoBehaviour
{
	public List<Transform> ToUnparent = new List<Transform>();

	void Start() {
		foreach (var gameObject in ToUnparent)
			gameObject.SetParent(null);
	}
}
