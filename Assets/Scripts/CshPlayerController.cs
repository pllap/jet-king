using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CshPlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    public float maxSpeed = 4.0f;
    public float maxThrust = 4.0f;
    public float thrustPower = 1.5f;

    private void Awake()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalMove();
        Thrust();
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
