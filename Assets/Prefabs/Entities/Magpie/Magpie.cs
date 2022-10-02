using System;
using System.Collections;
using System.Collections.Generic;
using Random = Unity.Mathematics.Random;
using UnityEngine;
using UnityEngine.Serialization;

public class Magpie : MonoBehaviour
{
    public float AttackDistance = 7;
    public float MoveDistance = 3;
    public float AttackMoveSpeed = 0.01f;
    static private Player _player;
    private static Random _rand = Random.CreateFromIndex(0);

    private Rigidbody2D _rb;
    private Vector2 _direction;
    private Animator _animator;
    private static readonly int Fly = Animator.StringToHash("Fly");
    private static readonly int Swoop = Animator.StringToHash("Swoop");
    public float AttackTimer { get; set; } = 0;
    public bool Attacking { get; set; } = false;
    private bool PlayerToRight = false;
    private SpriteRenderer _spriteRenderer;
    float Timer { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _player = Player.instance;
    }

    void NewDirection()
    {
        float x = _rand.NextFloat(-1, 1);
        float y = _rand.NextFloat(-1, 1);
        _direction = new Vector2(x, y) * MoveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Attacking)
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= 2)
            {
                Attacking = false;
                _animator.SetTrigger(Fly);
            }
            else
            {
                Vector2 temp = new Vector2((float)Math.Sin(-AttackTimer * Math.PI) * (PlayerToRight ? 1 : -1),
                    (float)Math.Cos(-AttackTimer * Math.PI));
                _rb.velocity +=
                    temp *
                    AttackMoveSpeed;
            }
        }
        else
        {
            PlayerToRight = _player.transform.position.x < transform.position.x;
            _spriteRenderer.flipX = !PlayerToRight;
            if (Vector2.Distance(_player.transform.position, transform.position)
                < AttackDistance)
            {
                Attacking = true;
                AttackTimer = 0;
                _animator.SetTrigger(Swoop);
            }

            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = _rand.NextFloat(3);
                NewDirection();
            }

            _rb.velocity =
                Vector3.RotateTowards(_rb.velocity, _direction,
                    Time.deltaTime, Time.deltaTime);
        }
    }
}