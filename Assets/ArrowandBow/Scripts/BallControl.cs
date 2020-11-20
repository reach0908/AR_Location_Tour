using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public float m_ThrowForce = 100f;

    public float m_ThrowDirectionX = 0.17f;
    public float m_ThrowDirectionY = 0.67f;

    public Vector3 m_BallCameraOffset = new Vector3(0f, -1.4f, 2f);

    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool directionChosen = false;
    private bool throwStarted = false;

    [SerializeField]
    GameObject ARCam;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;

    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        ResetBall();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            startTime = Time.time;
            throwStarted = true;
            directionChosen = false;
        }else if(Input.GetMouseButtonUp(0)){
            endTime = Time.time;
            duration = endTime - startTime;
            direction = Input.mousePosition - startPosition;
            directionChosen = true;
        }

        if (directionChosen){
            rb.mass = 1;
            rb.useGravity = true;

            rb.AddForce(
                ARCam.transform.forward * m_ThrowForce / duration +
                ARCam.transform.up * direction.y * m_ThrowDirectionY +
                ARCam.transform.right * direction.x * m_ThrowDirectionX);
            
            startPosition = new Vector3(0,0,0);
            direction = new Vector3(0, 0, 0);

            startTime = 0.0f;
            endTime = 0.0f;
            duration = 0.0f;

            directionChosen = false;
            throwStarted = false;
        }
        if (Time.time - endTime >= 5 && Time.time - endTime <= 6)
            ResetBall();
    }

    private void ResetBall()
    {
        rb.mass = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        endTime = 0.0f;

        Vector2 ballPos = ARCam.transform.position + ARCam.transform.forward * m_BallCameraOffset.z
            + ARCam.transform.up * m_BallCameraOffset.y;
        transform.position = ballPos;
    }
}
