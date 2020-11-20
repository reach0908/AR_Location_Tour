using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class ThrowingObject : MonoBehaviour
{
    
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    void Update()
    {
        #if UNITY_EDITOR       // 유니티 에디터인 경우만 실행됨
              if(Input.GetMouseButtonDown(1))
              {
                 startPos = Input.mousePosition;
                 Debug.Log("Start Pos: " + startPos.ToString());
              }

              if(Input.GetMouseButtonUp(1))
              {
                 float swipeDistVertical = (new Vector3(0, Input.mousePosition.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                 if (swipeDistVertical > minSwipeDistY)
                 {
                    float swipeValue = Mathf.Sign(Input.mousePosition.y - startPos.y);
                    if (swipeValue > 0)//up swipe
                    {
                       Awake();
                       Throw(swipeValue * 400700f);
                    }
               

                    Debug.Log("End Pos: " + Input.mousePosition.ToString());
                 }
              }
        #endif

        //위로 swipe하면 Awake 호출
        if (Input.touchCount > 0)
        {
            Debug.Log("Touched");
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    Debug.Log("Touched B");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touched E");
                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                    if (swipeDistVertical > minSwipeDistY)
                    {
                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                        if (swipeValue > 0)//up swipe
                        {
                            Awake();
                            Throw(swipeValue * 400700f);
                        }
                    }
                    break;
            }
        }
    }

    public Rigidbody rigidbody3D;
    private Collider[] colliders3D;
    public bool flagCustom = false;
    public MonoBehaviour monoBehaviourCustom;
    private float forceFactor = 1f;
    private Vector3 forceDirection;
    private Vector2 strength;
    private Vector2 strengthFactor;
    private CameraAxes forceDirectionExtra = CameraAxes.CameraMainTransformUp;
    private Vector3 forceDirectionExtraVector3;

    public enum CameraAxes
    {
        CameraMainTransformUp,
        CameraMainTransformForward,
        CameraMainTransformRight,
        CameraMainTransformUpRight,
        CameraMainTransformLeft,
        CameraMainTransformUpLeft
    }

    public CameraAxes torqueAxis = CameraAxes.CameraMainTransformRight;
    private Vector3 torqueAxisVector3;
    private float torqueAngleBasic;
    private float torqueAngle;
    private float torqueFactor;
    private Quaternion torqueRotation;
    private float maxAngularVelocityAtAwake = 7f;
    private bool isCenterOfMassByDefaultLoggedAtAwake;
    private bool isCenterOfMassCustomUsedAtAwake;
    private Vector3 centerOfMassCustomAtAwake;

    private Quaternion rotationByDefault;
    public enum RotationsForNextThrow
    {
        Default,
        Random,
        Custom
    }

    //position
    public Vector2 positionInViewportOnReset = new Vector2(0.5f, 0.1f);
    public float cameraNearClipPlaneFactorOnReset = 7.5f;
    private bool isObjectRotatedInThrowDirection = true;
    private RotationsForNextThrow rotationOnReset = RotationsForNextThrow.Default;
    private Vector3 rotationOnResetCustom = new Vector3(0f, 90f, 0f);
    public bool isThrown = false;
    public event Action OnThrow;
    public event Action OnResetPhysicsBase;

    private void Awake()
    {
        rigidbody3D.maxAngularVelocity = maxAngularVelocityAtAwake;
        if (isCenterOfMassByDefaultLoggedAtAwake)
        {
            print("[Center Of Mass] X: " + rigidbody3D.centerOfMass.x);
            print("[Center Of Mass] Y: " + rigidbody3D.centerOfMass.x);
            print("[Center Of Mass] Z: " + rigidbody3D.centerOfMass.z);
        }
        if (isCenterOfMassCustomUsedAtAwake)
        {
            rigidbody3D.centerOfMass = centerOfMassCustomAtAwake;
        }
        colliders3D = GetComponentsInChildren<Collider>();
        rotationByDefault = rigidbody3D.rotation;
    }
    public void Throw(float a_Power)
    {
        rigidbody3D.isKinematic = false;
        ThrowBase(Vector2.zero, new Vector2(0, a_Power), new Vector2(1, 1), Camera.main.transform, Screen.height, 0, 0, 0);
    }

    public void ThrowBase(
        Vector2 inputPositionFirst,
        Vector2 inputPositionLast,
        Vector2 inputSensitivity,
        Transform cameraMain,
        int screenHight,
        float forceFactorExtra,
        float torqueFactorExtra,
        float torqueAngleExtra)
    {
        strengthFactor = inputPositionLast - inputPositionFirst;

        if (inputPositionLast.y < screenHight / 2 && Mathf.Abs(strengthFactor.y) > 0f)
        {
            strengthFactor.x *= inputPositionLast.y / strengthFactor.y;

            //print("[Correction] strengthFactor")
        }

        strengthFactor /= screenHight;
        strength.y = inputSensitivity.y * strengthFactor.y;
        strength.x = inputSensitivity.x * strengthFactor.x;
        forceDirection = new Vector3(strength.x, 0f, 1f);
        forceDirection = cameraMain.transform.TransformDirection(forceDirection);
        torqueAngleBasic = Mathf.Sign(strengthFactor.x)
            * Vector3.Angle(cameraMain.transform.forward, forceDirection);
        torqueRotation = Quaternion.AngleAxis(
            torqueAngleBasic + torqueAngle + torqueAngleExtra,
            cameraMain.transform.up);
        rigidbody3D.useGravity = true;
        forceDirectionExtraVector3 = GetCameraAxis(cameraMain, forceDirectionExtra);
        rigidbody3D.AddForce(
            (forceDirection + forceDirectionExtraVector3)
            * (forceFactor + forceFactorExtra)
            * strength.y);
        if (isObjectRotatedInThrowDirection)
        {
            rigidbody3D.rotation =
                Quaternion.AngleAxis(
                    Mathf.Sign(strengthFactor.x) * Vector3.Angle(cameraMain.transform.forward, forceDirection),
                    cameraMain.transform.up)
                * rigidbody3D.rotation;
        }
        torqueAxisVector3 = GetCameraAxis(cameraMain, torqueAxis);
        rigidbody3D.AddTorque(torqueRotation * torqueAxisVector3 * (torqueFactor + torqueFactorExtra));
        if (OnThrow != null)
        {
            OnThrow.Invoke();
        }
    }
    private Vector3 GetCameraAxis(Transform cameraMain, CameraAxes cameraAxis)
    {
        switch (cameraAxis)
        {
            case CameraAxes.CameraMainTransformUp:
                return cameraMain.transform.up;
            case CameraAxes.CameraMainTransformForward:
                return cameraMain.transform.forward;
            case CameraAxes.CameraMainTransformRight:
                return cameraMain.transform.right;
            case CameraAxes.CameraMainTransformUpRight:
                return cameraMain.transform.right + cameraMain.transform.up;
            case CameraAxes.CameraMainTransformLeft:
                return cameraMain.transform.right * -1f;
            case CameraAxes.CameraMainTransformUpLeft:
                return cameraMain.transform.right * -1f + cameraMain.transform.up;
            default:
                return Vector3.zero;
        }
    }
    public void ResetPhysicsBase()
    {
        //Debug.Log("ResetPhysics()");
        rigidbody3D.useGravity = false;
        rigidbody3D.velocity = Vector3.zero;
        rigidbody3D.angularVelocity = Vector3.zero;
        if (OnResetPhysicsBase != null)
        {
            OnResetPhysicsBase.Invoke();
        }
    }
    public void ResetPosition(Camera cameraMain)
    {
        rigidbody3D.position =
            cameraMain.ViewportToWorldPoint(
                new Vector3(
                    positionInViewportOnReset.x,
                    positionInViewportOnReset.y,
                    cameraMain.nearClipPlane * cameraNearClipPlaneFactorOnReset));
    }
    public void ResetRotation(Transform parent)
    {
        switch (rotationOnReset)
        {
            case RotationsForNextThrow.Default:
                if (parent)
                {
                    rigidbody3D.rotation = parent.rotation * rotationByDefault;
                }
                else
                {
                    rigidbody3D.rotation = rotationByDefault;
                }
                break;
            case RotationsForNextThrow.Random:
                rigidbody3D.rotation = GetRandomRotation();
                break;
            case RotationsForNextThrow.Custom:
                if (parent)
                {
                    rigidbody3D.rotation = parent.rotation * Quaternion.Euler(rotationOnResetCustom);
                }
                else
                {
                    rigidbody3D.rotation = Quaternion.Euler(rotationOnResetCustom);
                }
                break;
        }
    }
    private Quaternion GetRandomRotation()
    {
        Quaternion randomRotation = new Quaternion();
        randomRotation.eulerAngles = new Vector3(
            UnityEngine.Random.Range(0f, 360f),
            UnityEngine.Random.Range(0f, 360f),
            UnityEngine.Random.Range(0f, 360f));
        return randomRotation;
    }
}