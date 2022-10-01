using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingEntity : MonoBehaviour
{
	private Vector2 _velocity = new Vector2();
	private Vector3 _position = new Vector3();
	public void Save() {
		_velocity = _rb.velocity;
		_position = _rb.position;
	}
	public void Load() {
		_rb.velocity = _velocity;
		_rb.position = _position;
	}

	protected Rigidbody2D _rb;
	void Start() {
		_rb = GetComponent<Rigidbody2D>();
	}
}
