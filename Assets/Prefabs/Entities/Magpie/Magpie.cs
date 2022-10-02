using System;
using System.Collections;
using System.Collections.Generic;
using Random = Unity.Mathematics.Random;
using UnityEngine;
using UnityEngine.Serialization;

public class Magpie : LoopingEntity
{
    public float AttackDistance = 8;
    public float RandomTargetDistance = 3;
    public float WanderRange = 12;
    public float AccelerationSpeed = 1;
    public float TurnSpeed = 1;
    public float MaxSpeed = 2;
    static private Player _player;
    private static Random _rand = Random.CreateFromIndex(0);

    private Rigidbody2D _rb;
    private Vector2 _target;
    private Animator _animator;
    private static readonly int Fly = Animator.StringToHash("Fly");
    private static readonly int Swoop = Animator.StringToHash("Swoop");
    private bool PlayerToRight = false;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _origin;
    float Timer { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _origin = _rb.transform.position;
        _player = Player.instance;
    }

    void NewDirection()
    {
        float x = _rand.NextFloat(-1, 1);
        float y = _rand.NextFloat(-1, 1);
        _target = _rb.position + (new Vector2(x, y) * RandomTargetDistance);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        PlayerToRight = _player.transform.position.x < _rb.position.x;
        _spriteRenderer.flipX = !PlayerToRight;
        Debug.DrawLine(_rb.position, _target, Color.gray);
        // Find new direction
        if (Timer <= 0)
        {
            Timer = _rand.NextFloat(3);
            NewDirection();
        }

        // Return if wandered too far
        if (Vector2.Distance(_origin, _rb.position)
            > WanderRange)
        {
            _target = _origin;

            Debug.DrawLine(transform.position, _target, Color.blue);
        }

        // Attack if player close
        if (Vector2.Distance(_player.transform.position, _rb.position)
            < AttackDistance)
        {
            _target = _player.transform.position;
            _animator.SetTrigger(Swoop);
            Debug.DrawLine(transform.position, _target, Color.red);
        }
        else
        {
            _animator.SetTrigger(Fly);
        }

        _rb.velocity = Vector2.ClampMagnitude(
            Vector3.RotateTowards(_rb.velocity, _target-_rb.position,
                Time.deltaTime * TurnSpeed, Time.deltaTime * AccelerationSpeed),
            MaxSpeed);
    }

}