using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CshPlayerController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalAcceleration;
    public float thrust;
    public float thrustAcceleration;

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
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
        Thrust();
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
            if (CshGameManager.Instance.thrustable)
            {
                CshGameManager.Instance.isThrusting = true;
                _rigidbody2D.AddForce(thrustAcceleration * Vector2.up, ForceMode2D.Force);
            }

            if (_rigidbody2D.velocity.y > thrust)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, thrust);
            }
        }
        else
        {
            CshGameManager.Instance.isThrusting = false;
        }
    }

    private void HorizontalMove()
    {
        var direction = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.AddForce(direction * horizontalAcceleration * Vector2.right, ForceMode2D.Force);

        if (_rigidbody2D.velocity.x * direction > horizontalSpeed)
        {
            _rigidbody2D.velocity = new Vector2(direction * horizontalSpeed, _rigidbody2D.velocity.y);
        }
    }
}