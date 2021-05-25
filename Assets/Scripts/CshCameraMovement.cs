using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CshCameraMovement : MonoBehaviour
{
    public Camera mCamera;
    public Transform mPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = mPlayer.position.y - mCamera.transform.position.y;
        mCamera.transform.position = new Vector2(mCamera.transform.position.x, mPlayer.position.y);
    }
}
