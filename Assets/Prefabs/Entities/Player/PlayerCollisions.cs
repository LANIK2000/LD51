using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : LoopingEntity
{
	public void onGroundEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround = true;
				_coyoteTime = CoyoteTime;
				break;
			default:
				break;
		}
	}

	public void onGroundExit(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround = false;
				break;
			default:
				break;
		}
	}

	public void onWallLeftEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallL = true;
				_coyoteTime = CoyoteTime;
				break;
			default:
				break;
		}
	}

	public void onWallLeftExit(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallL = false;
				break;
			default:
				break;
		}
	}

	public void onWallRightEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallR = true;
				_coyoteTime = CoyoteTime;
				break;
			default:
				break;
		}
	}

	public void onWallRightExit(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallR = false;
				break;
			default:
				break;
		}
	}
}
