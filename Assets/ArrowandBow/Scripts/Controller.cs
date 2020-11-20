using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class Controller : MonoBehaviour
{

    public GameObject Arrow;
    public GameObject Bow;
    public Button NockingBtn;
    public static int chances = 4;
    public static bool isNotched;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;

    [SerializeField]
    GameObject ARCam;

    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool directionChosen = false;
    private GameObject lives;

    Rigidbody Arrow_rb;

    public float m_ThrowForce = 50f;
    public float m_ThrowDirectionX = 0.17f;
    public float m_ThrowDirectionY = 0.67f;


    public Vector3 m_CameraOffset = new Vector3(0.1f, -0.2f, 0.5f);

    void Start()
    {
        Button btn = NockingBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        lives = GameObject.Find("Lives");
        //ResetArrow();
    }
    void TaskOnClick()
    {
        Debug.Log("Nocking");
        if(chances > 0)
        {
            ResetArrow();
            Debug.Log("Button : Clicked " + isNotched);
            GameObject life = lives.transform.GetChild(chances - 1).gameObject;

            chances--;

            Destroy(life);
        }
        else
        {
            int winCount = 0;
            Debug.Log("게임종료!");
            PlayerPrefs.SetInt("arrowGame", 1234);
            
            if(KeepScore.Score >= 15)
            {
                winCount = 3;
            }
            else if(KeepScore.Score >= 10)
            {
                winCount = 2;
            }
            else if(KeepScore.Score >= 5 )
            {
                winCount = 1;
            }
            else
            {
                winCount = 0;
            }
            PlayerPrefs.SetInt("arrowWinScore", winCount);
            //Destroy(btn);
            ARGameManager.instance.Game_resultcv();


        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //RotateArrow();
        if (isNotched)
            {
            bool down = false;
            bool up = false;
            //mobile
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && 
                    !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    down = true;
                }
                if (touch.phase == TouchPhase.Ended &&
                    !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    up = true;
                }
                
            }
#endif
#if UNITY_EDITOR
            //editor
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                down = true;
            }
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                up = true;
            }
#endif

            if (down)
            { 
                Debug.Log("GeMouseButtonDown");

                Arrow_rb.isKinematic = true;

                startPosition = Input.mousePosition;
                startTime = Time.time;
                directionChosen = false;

            //Bow.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, (FlightVector.magnitude - 0.18f) * 125);
            }
            if (up) 
            {
                Debug.Log("GeMouseButtonUp");
                Debug.Log(direction.magnitude);
            
                Arrow.transform.parent = null;

                Arrow_rb.isKinematic = false;

                endTime = Time.time;
                duration = endTime - startTime;
                direction = Input.mousePosition - startPosition;
            
                directionChosen = true;
                //Bow.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

            }

            if (directionChosen)
            {
                Arrow_rb.mass = 1;
                Arrow_rb.useGravity = true;

                /*Arrow_rb.AddForce(
                    ARCam.transform.forward * m_ThrowForce / duration +
                    ARCam.transform.up * direction.y * m_ThrowDirectionY +
                    ARCam.transform.right * direction.x * m_ThrowDirectionX);*/
                Arrow_rb.AddForce(ARCam.transform.forward * m_ThrowForce * direction.magnitude);

                startPosition = new Vector3(0, 0, 0);
                direction = new Vector3(0, 0, 0);

                startTime = 0.0f;
                endTime = 0.0f;
                duration = 0.0f;

                directionChosen = false;
                isNotched = false;
            }
        }


    }

    public void RotateArrow()
    {
        if (isNotched)
        {
            Arrow.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void ResetArrow()
    {
        if (!isNotched)
        {
            Arrow_rb = Arrow.GetComponent<Rigidbody>();
            m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
            //ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;

            Arrow_rb.mass = 0;
            Arrow_rb.useGravity = false;
            Arrow_rb.isKinematic = true;
            Arrow_rb.velocity = Vector3.zero;
            Arrow_rb.angularVelocity = Vector3.zero;
            endTime = 0.0f;

            /*Vector2 ArrowPos = ARCam.transform.position + ARCam.transform.forward * m_BallCameraOffset.z
                + ARCam.transform.up * m_BallCameraOffset.y;
            transform.position = ArrowPos;*/
            Arrow.transform.parent = Camera.main.transform;
            
            Arrow.transform.localPosition = m_CameraOffset;
            Arrow.transform.localRotation = Quaternion.identity;

            Debug.Log("Arrow.transform.localPosition" + Arrow.transform.localPosition);

            isNotched = true;
        }
      
    }


}
