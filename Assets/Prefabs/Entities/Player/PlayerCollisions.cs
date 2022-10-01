using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : LoopingEntity
{
	public void onGroundEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround++;
				_coyoteTime = CoyoteTime;
				break;
			default:
				break;
		}
	}

	public void onGroundExit(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
				_onGround--;
				break;
			default:
				break;
		}
	}

	public void onWallLeftEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallL++;
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
				_onWallL--;
				break;
			default:
				break;
		}
	}

	public void onWallRightEnter(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Ground":
			case "Wall":
				_onWallR++;
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
				_onWallR--;
				break;
			default:
				break;
		}
	}
}
