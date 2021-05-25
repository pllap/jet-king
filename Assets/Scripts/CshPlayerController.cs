using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CshPlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public float maxSpeed = 4.0f;
    public float maxThrust = 2.0f;
    public float thrustPower = 1.0f;

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
            _spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1.0f;
        }

        _animator.SetBool("isRunning", Input.GetButton("Horizontal"));
    }

    private void HorizontalMove()
    {
        var direction = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.AddForce(Vector2.right * direction, ForceMode2D.Force);

        if (_rigidbody2D.velocity.x * direction > maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(direction * maxSpeed, _rigidbody2D.velocity.y);
        }
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody2D.AddForce(Vector2.up * thrustPower, ForceMode2D.Force);

            if (_rigidbody2D.velocity.y > maxThrust)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, maxThrust);
            }
        }
    }
}