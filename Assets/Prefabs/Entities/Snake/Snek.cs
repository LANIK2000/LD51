using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Snek : MonoBehaviour
{
    public float MoveForce = 6;
    public float MoveFrequency = 1f;
    public int RotationFrequency = 50;
    private bool FacingRight = false;

    public void seesPit(Collider2D cl)
    {
        _rb.velocity = Vector2.zero;
        Rotate();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    float Timer { get; set; } = 0;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Wait = Animator.StringToHash("Wait");

    private static Random _rand = Random.CreateFromIndex(0);

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            _rb.velocity += new Vector2( MoveForce * (FacingRight ? 1 : -1), 0);
            Timer = MoveFrequency;
            if (_rand.NextInt(RotationFrequency) == 0)
                Rotate();
        }
    }

    void Rotate()
    {
        FacingRight = !FacingRight;
        _spriteRenderer.flipX = FacingRight;
    }
}