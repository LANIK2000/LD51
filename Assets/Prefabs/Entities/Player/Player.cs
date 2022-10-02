using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class Player : LoopingEntity
{
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
	private Animator _animator;

	protected override void Start() {
		_animator = GetComponent<Animator>();
		base.Start();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_groundTrigger = transform.Find("GroundTrigger").GetComponent<BoxCollider2D>();
		_timerTextMesh = transform.Find("Canvas").Find("Timer").GetComponent<TMP_Text>();
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

		_animator.SetBool(AnimAttack, Input.GetButtonDown("Attack"));
		_animator.SetFloat(AnimSpeed, Math.Abs(_rb.velocity.x));
		_animator.SetFloat(AnimVerticality, _rb.velocity.y);
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

		if (speed > 0)
			_spriteRenderer.flipX = false;
		else if (speed < 0)
			_spriteRenderer.flipX = true;

		_rb.velocity = new Vector2(speed, _rb.velocity.y) + _velocity_overide;
	}
}
