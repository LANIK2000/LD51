using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class Player : LoopingEntity
{
	public static Player instance { get; set; } = null;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			// this.Dispose();
		}
		else
			instance = this;
	}	

	public float RunningSpeed = 15;
	public float JumpForce = 15;

	public Transform Camera;
	public float CameraFollowSpeed = 1;

	public Transform CheckPoint;

	public override void Save() {
		base.Save();
		if (CheckPoint != null) {
			CheckPoint.position = _rb.position;
			CheckPoint.position = new Vector3(CheckPoint.position.x, CheckPoint.position.y, -1);
		}
	}

	public override void Load() {
		base.Load();
		if (Camera != null)
			Camera.position = _rb.position;
	}

	public bool Flip { get; private set; } = false;

	TMP_Text _timerTextMesh;
	SpriteRenderer _spriteRenderer;
	public float CoyoteTime = .25f;
	float _coyoteTime = 0;
	int _onGround = 0;
	int _onWallL = 0;
	int _onWallR = 0;
	Vector2 _velocity_overide = new Vector2();
	BoxCollider2D _groundTrigger;
	private static readonly int AnimSpeed = Animator.StringToHash("Speed");
	private static readonly int AnimVerticality = Animator.StringToHash("Verticality");
	private static readonly int AnimAttack = Animator.StringToHash("AnimAttack");
	private static readonly int AnimAttackInit = Animator.StringToHash("AnimAttackInit");
	private static readonly int AnimOnGround = Animator.StringToHash("onGround");
	private Animator _animator;
	GameObject _attackLeft;
	GameObject _attackRight;
	bool _canAttack = true;

	protected override void Start() {
		_animator = GetComponent<Animator>();
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_groundTrigger = transform.Find("GroundTrigger").GetComponent<BoxCollider2D>();
		_timerTextMesh = transform.Find("Canvas").Find("Timer").GetComponent<TMP_Text>();
		_attackLeft = transform.Find("AttackLeft").gameObject;
		_attackRight = transform.Find("AttackRight").gameObject;
		var Canvas = transform.Find("Canvas");
		if (Canvas != null)
			Canvas.parent = null;
		if (CheckPoint != null && CheckPoint.parent != null)
			CheckPoint.parent = null;
		if (Camera != null && Camera.parent != null)
			Camera.parent = null;
	}

	void Update() {
		_timerTextMesh.text = "Reset Timer: " + (LoopSaveSystem.instance?.Timer ?? 0).ToString();

		if (_onGround == 0 && _onWallL == 0 && _onWallR == 0)
			_coyoteTime -= Time.deltaTime;

		if (Input.GetButtonDown("Jump") && _coyoteTime > 0) {
			_rb.velocity = new Vector2(_rb.velocity.x, JumpForce) + _velocity_overide;
			if (_onGround == 0) {
				if (_onWallL > 0)
					_velocity_overide.x = RunningSpeed * 1.25f;
				else if (_onWallR > 0)
					_velocity_overide.x = -RunningSpeed * 1.25f;
				else
					_coyoteTime = 0;
			}
		}

		if (Input.GetButtonDown("Reset"))
			LoopSaveSystem.instance?.LoadAll();

		if (_canAttack && Input.GetButtonDown("Attack")) {
			if (Input.GetButton("Left"))
				Flip = true;
			else if (Input.GetButton("Right"))
				Flip = false;
			_canAttack = false;
			_animator.SetBool(AnimAttackInit, true);
			_animator.SetBool(AnimAttack, true);
		}
		_animator.SetBool(AnimOnGround, _onGround > 0);
		_animator.SetFloat(AnimSpeed, Math.Abs(_rb.velocity.x));
		_animator.SetFloat(AnimVerticality, _rb.velocity.y);
		
		_spriteRenderer.flipX = Flip;
	}

	public void AttackStrikeStart() {
		if (Flip)
			_attackLeft.SetActive(true);
		else
			_attackRight.SetActive(true);
	}

	public void AttackStrikeEnd() {
		_canAttack = true;
		_attackLeft.SetActive(false);
		_attackRight.SetActive(false);
	}

	public void AttackEnd() {
		_animator.SetBool(AnimAttack, false);
	}

	void FixedUpdate() {
		if (Camera != null) {
			Camera.position = Vector2.Lerp(Camera.position, transform.position, CameraFollowSpeed * Time.deltaTime);
			Camera.position = new Vector3(Camera.position.x, Camera.position.y, -2);
		}

		_velocity_overide = Vector2.Lerp(_velocity_overide, new Vector2(), 8 * Time.deltaTime);
		float overide_x = 1 - Mathf.Abs(_velocity_overide.x / RunningSpeed);
		float speed = Input.GetAxis("Horizontal") * RunningSpeed;

		if (speed > 0) {
			if (_velocity_overide.x < 0)
				speed *= overide_x;
		}
		else if (speed < 0) {
			if (_velocity_overide.x > 0)
				speed *= overide_x;
		}

		if (MathF.Abs(speed + _velocity_overide.x) > .1f && !_animator.GetBool(AnimAttack))
			Flip = (speed + _velocity_overide.x) < 0;

		_rb.velocity = new Vector2(speed, _rb.velocity.y) + _velocity_overide;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.tag);
		if (other.tag == "EnemyAttack")
			LoopSaveSystem.instance?.LoadAll();
	}
}
