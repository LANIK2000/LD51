using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : LoopingEntity
{
	public Web Web;

	public override void Save() {
		base.Save();
		_saved_alive = _alive;
	}

	public override void Load() {
		base.Load();
		_alive = _saved_alive;
		gameObject.SetActive(_saved_alive);
	}

	bool _saved_alive = true;
	bool _alive = true;
	void Die(){
		_alive = false;
		gameObject.SetActive(false);
	}

	public float MoveSpeed = 1;

	void Update() {
		Vector2 target;
		if (Web.PlayerInWeb)
			target = Player.instance.transform.position;
		else
			target = transform.parent.position;
		
		if (Vector2.Distance(target, transform.position) > .1f)
			_rb.velocity = (target - _rb.position).normalized * MoveSpeed;
		else
			_rb.velocity = Vector2.zero;
		
		// transform.rotation = Quaternion.Euler(0, 0,
		// 	Lib.FindAngle(transform.position, target) - 90);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "PlayerAttack")
			this.Die();
		if (other.tag == "Player")
			LoopSaveSystem.instance.LoadAll();
	}
}
