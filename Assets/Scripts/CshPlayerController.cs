using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CshPlayerController : MonoBehaviour
{
    public float maxSpeed;
    private float normalSpeed = 0.0f;
    private float slowSpeed = 0.0f;
    public float _horizontalAcceleration = 16.0f;
    public float _currentSpeed = 0.0f;

    public float maxThrust;
    public float _thrustAcceleration = 32.0f;
    private float _currentThrust = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = maxSpeed;
        slowSpeed = maxSpeed * 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        Thrust();
    }

    private void HorizontalMove()
    {        
        // slow speed down when shift button is being pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed = slowSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = normalSpeed;
        }
        
        // idle
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (_currentSpeed > 0.0f)
            {
                _currentSpeed -= _horizontalAcceleration * Time.deltaTime;
            }
            if (_currentSpeed < 0.0f)
            {
                _currentSpeed += _horizontalAcceleration * Time.deltaTime;
            }
        }
        
        // under specific value of speed are set to 0
        if (Mathf.Abs(_currentSpeed) < 0.1f)
        {
            _currentSpeed = 0.0f;
        }

        // left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_currentSpeed > -maxSpeed)
            {
                _currentSpeed -= _horizontalAcceleration * Time.deltaTime;
            }
        }
        
        // right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_currentSpeed < maxSpeed)
            {
                _currentSpeed += _horizontalAcceleration * Time.deltaTime;
            }
        }
        
        transform.position = new Vector2(transform.position.x + _currentSpeed * Time.deltaTime, transform.position.y);
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_currentThrust < maxThrust)
            {
                _currentThrust += _thrustAcceleration * Time.deltaTime;
            }
        }
        else
        {
            if (_currentThrust > 0.0f)
            {
                _currentThrust -= _thrustAcceleration * Time.deltaTime;
            }

            if (_currentThrust < 0.0f)
            {
                _currentThrust = 0.0f;
            }
        }
        
        transform.position = new Vector2(transform.position.x, transform.position.y + _currentThrust * Time.deltaTime);
    }
}
