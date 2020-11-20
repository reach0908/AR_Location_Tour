using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceHoop : MonoBehaviour
{
   
    [SerializeField]
    [Tooltip("Instantiates this hoop prefab on a plane at the touch location.")]
    GameObject m_HoopPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedHoop
    {
        get { return m_HoopPrefab; }
        set { m_HoopPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedHoop { get; private set; }

    [SerializeField]
    [Tooltip("Instantiates this ball prefab in front of the AR Camera.")]
    GameObject m_ArrowPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedBall
    {
        get { return m_ArrowPrefab; }
        set { m_ArrowPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedArrow { get; private set; }

    [SerializeField]
    [Tooltip("Instantiates this ball prefab in front of the AR Camera.")]
    GameObject m_BowGO;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedBall2
    {
        get { return m_BowGO; }
        set { m_BowGO = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedBall2 { get; private set; }

    [SerializeField]
    GameObject planePrefab;
    public GameObject placedPlane
    {
        get { return planePrefab; }
        set { planePrefab = value; }
    }
    public GameObject spawnedPlane { get; private set; }


    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;


    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public Vector3 m_CameraOffset = new Vector3(0.1f, -0.2f, 0.5f);
    private bool isPlaced = false;

    void RotateTowardCamera(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Vector3 vecProj = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
            float angle = Vector3.SignedAngle(gameObject.transform.right, vecProj, Vector3.up);
            //ARCAM ANGLE 따라가기
            gameObject.transform.Rotate(0f, angle, 0f, Space.World);
        }
    }

    void Start()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
#if UNITY_EDITOR
        spawnedPlane = Instantiate(planePrefab);
#endif
    }

    public Controller controller;
    void Update()
    {
#if UNITY_EDITOR
        if (isPlaced)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit, 100f);

            if (hit.transform != null)
            {
                Debug.DrawLine(transform.position, hit.point, Color.yellow);
                Debug.Log("Hit!! : " + hit.point.ToString());

                if (spawnedHoop == null)
                {
                    spawnedHoop = Instantiate(m_HoopPrefab, hit.point, Quaternion.identity);
                    RotateTowardCamera(spawnedHoop);
                    

                    spawnedArrow = Instantiate(m_ArrowPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
                    spawnedArrow.transform.parent = Camera.main.transform;


                    controller.Arrow = spawnedArrow;
                    isPlaced = true;
                    Controller.isNotched = false;
                    controller.ResetArrow();

                    //점수 체크 스크립트 추가
           
                    //spawnedHoop.GetComponent<CheckScore>().controller = controller;
                    //spawnedHoop.GetComponent<CheckScore>().Center = hit.point;

                    //spawnedArrow.transform.localPosition = Vector3.zero; //위치
                    //spawnedArrow.transform.localRotation = Quaternion.identity; //

                    m_BowGO.transform.parent = Camera.main.transform;
                    m_BowGO.transform.localPosition = m_CameraOffset;
                    m_BowGO.transform.localRotation = Quaternion.identity;
                    //RotateTowardCamera(spawnedHoop);

                }
                
                return;
            }
            else
            {
                Debug.Log("No Hit!!");
            }
        }
#endif 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    Debug.DrawLine(transform.position, hitPose.position, Color.yellow);
                    Debug.Log("Hit!! : " + hitPose.position.ToString());

                    if (spawnedHoop == null)
                    {
                        spawnedHoop = Instantiate(m_HoopPrefab, hitPose.position, Quaternion.identity);
                        RotateTowardCamera(spawnedHoop);
                        
                        spawnedArrow = Instantiate(m_ArrowPrefab);
                        spawnedArrow.transform.parent = Camera.main.transform;

                        controller.Arrow = spawnedArrow;
                        isPlaced = true;
                        Controller.isNotched = false;
                        controller.ResetArrow();

                        m_BowGO.transform.parent = Camera.main.transform;
                        m_BowGO.transform.localPosition = new Vector3(0, 0, 0);
                        m_BowGO.transform.localRotation = Quaternion.identity;
                        //RotateTowardCamera(spawnedHoop);

                        return;
                    }

                }
                else
                {
                    Debug.Log("No Hit!!");
                }
            }
        }
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    spawnedHoop = Instantiate(m_HoopPrefab, hitPose.position, Quaternion.AngleAxis(180, Vector3.up));
                    spawnedHoop.transform.parent = transform.parent;
                    //RotateTowardCamera functioi 
                    isPlaced = true;

                    spawnedArrow = Instantiate(m_ArrowPrefab);
                    spawnedArrow.transform.parent = m_RaycastManager.transform.Find("AR Camera").gameObject.transform;

                    spawnedBall2 = Instantiate(m_BowGO);
                    spawnedBall2.transform.parent = m_RaycastManager.transform.Find("AR Camera").gameObject.transform;

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }*/
    }
}
