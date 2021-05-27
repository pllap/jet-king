using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CshPlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float maxThrust;
    public float thrustPower;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        this._animator = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalMove();
        Thrust();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            var facing = Input.GetAxisRaw("Horizontal");
            if (facing == 1.0f)
            {
                _spriteRenderer.flipX = false;
            }
            else if (facing == -1.0f)
            {
                _spriteRenderer.flipX = true;
            }
            else if (facing == 0.0f)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        }

        _animator.SetBool("isRunning", Input.GetButton("Horizontal"));

        // landing
        _animator.SetBool(
            "isFloating",
            Physics2D.Raycast(_rigidbody2D.position, Vector2.down, 0.5f).collider == null
        );
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody2D.AddForce(Time.deltaTime * thrustPower * Vector2.up, ForceMode2D.Force);

            if (_rigidbody2D.velocity.y > maxThrust)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, maxThrust);
            }
        }
    }

    private void HorizontalMove()
    {
        var direction = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.AddForce(Time.deltaTime * direction * acceleration * Vector2.right, ForceMode2D.Force);

        if (_rigidbody2D.velocity.x * direction > maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(direction * maxSpeed, _rigidbody2D.velocity.y);
        }
    }
}