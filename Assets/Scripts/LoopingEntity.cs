using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingEntity : MonoBehaviour
{
	private Vector2 _velocity = new Vector2();
	private Vector2 _position = new Vector2();
	private float _rotation = 0;
	public virtual void Save() {
		_velocity = _rb.velocity;
		_position = _rb.position;
		_rotation = _rb.rotation;
	}
	public virtual void Load() {
		_rb.velocity = _velocity;
		_rb.position = _position;
		_rb.rotation = _rotation;
	}

	protected Rigidbody2D _rb;
	protected virtual void Start() {
		_rb = GetComponent<Rigidbody2D>();
		LoopSaveSystem.Entities.Add(this);
	}
}
