using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lib {
	static public float FindAngle(Vector2 from, Vector2 to) {
		float xDiff = from.x - to.x;
		float yDiff = from.y - to.y;
		return Mathf.Atan2(yDiff, xDiff) * (float)(180 / Mathf.PI);
	}
}
