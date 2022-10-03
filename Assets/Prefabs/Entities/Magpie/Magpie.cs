using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magpie : LoopingEntity
{
	public override void Save() {
		_saved_alive = _alive;
		_savedTime = _wanderTimer;
		_savedVelocity = _velocity;
		_savedAggro = _aggro;
		base.Save();
	}

	public override void Load() {
		_alive = _saved_alive;
		gameObject.SetActive(_saved_alive);
		_wanderTimer = _savedTime;
		_velocity = _savedVelocity;
		_aggro = _savedAggro;
		base.Load();
	}

	bool _saved_alive = true;
	bool _alive = true;
	void Die(){
		_alive = false;
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "PlayerAttack")
			this.Die();
		if (other.tag == "Player")
			LoopSaveSystem.instance.LoadAll();
	}

	public float WanderDistanceY = 3;
	public float WanderDistanceX = 10;
	public float WanderTimer = 1;
	public float AggroDistance = 3;
	public float MaxSpeed = 5;
	public float MaxAggroSpeed = 10;
	bool _aggro = false;
	bool _savedAggro = false;

	private Animator _animator;
	private static readonly int AnimAttack = Animator.StringToHash("Attack");
	SpriteRenderer _spriteRenderer;
	Vector2 _origin;
	Vector2 _velocity = new Vector2();
	Vector2 _savedVelocity = new Vector2();
	Vector2 _wanderDir = new Vector2();
	float _wanderTimer = 0;
	float _savedTime = 0;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		_origin = transform.parent.position;
	}

	void Update() {
		if (_aggro) {
			_velocity += (new Vector2(Player.instance.transform.position.x, Player.instance.transform.position.y)
				- _rb.position).normalized * Time.deltaTime * 10;
			if (Mathf.Abs(_velocity.x) + Mathf.Abs(_velocity.y) > MaxAggroSpeed) {
				_velocity.Normalize();
				_velocity *= MaxAggroSpeed;
			}
		}
		else {
			if (Vector2.Distance(_rb.position, Player.instance.transform.position) < AggroDistance)
				_aggro = true;

			_wanderTimer -= Time.deltaTime;
			if (_wanderTimer < 0) {
				_wanderTimer = WanderTimer;
				_wanderDir = new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f));
			}

			if (Mathf.Abs(_origin.x - _rb.position.x) > WanderDistanceX
				|| Mathf.Abs(_origin.y - _rb.position.y) > WanderDistanceY) {
					_velocity += (_origin - _rb.position).normalized * Time.deltaTime * 4;
					_wanderTimer = WanderTimer;
					_wanderDir = -_velocity;
			}
			else
				_velocity += _wanderDir * Time.deltaTime * 2;
			
			if (Mathf.Abs(_velocity.x) + Mathf.Abs(_velocity.y) > MaxSpeed) {
				_velocity.Normalize();
				_velocity *= MaxSpeed;
			}
		}


		_spriteRenderer.flipX = _velocity.x > 0;
		_rb.velocity = _velocity;
		_animator.SetBool(AnimAttack, _aggro);
	}
}
