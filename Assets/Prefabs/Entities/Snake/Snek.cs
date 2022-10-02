using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Snek : LoopingEntity
{
	public float MoveForce = 6;
	public float MoveFrequency = 1f;
	private bool FacingRight = false;
	bool _savedFlip = false;
	float Timer = 0;
	float _savedTime = 0;

	public override void Save() {
		base.Save();
		_saved_alive = _alive;
		_savedFlip = FacingRight;
		_savedTime = Timer;
	}

	public override void Load() {
		base.Load();
		_alive = _saved_alive;
		gameObject.SetActive(_saved_alive);
		FacingRight = _savedFlip;
		_spriteRenderer.flipX = FacingRight;
		Timer = _savedTime;
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

	public void seesPit(Collider2D cl)
	{
		if (cl.tag == "Ground")
		{
			_rb.velocity = Vector2.zero;
			Rotate();
		}
	}

	public void seesWall(Collider2D cl)
	{
		if (cl.tag == "Ground")
		{
			_rb.velocity = Vector2.zero;
			Rotate();
		}
	}

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();
	}

	private SpriteRenderer _spriteRenderer;

	// Update is called once per frame
	void Update()
	{
		Timer -= Time.deltaTime;
		if (Timer <= 0)
		{
			_rb.velocity = new Vector2( MoveForce * (FacingRight ? 1 : -1), 0);
			Timer = MoveFrequency;
		}
	}

	void Rotate()
	{
		FacingRight = !FacingRight;
		_spriteRenderer.flipX = FacingRight;
	}
}
