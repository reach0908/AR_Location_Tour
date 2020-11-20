using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MarkerController : MonoBehaviour
{
    public Button destination1;
    public Button destination2;
    public Button destination3;
    public Button destination4;

    public ConnectToArNavi m_connection;

    private int destination;

    [SerializeField]
    [Tooltip("Instantiates this place info prefab when ")]
    GameObject m_pInfoPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placePIfo
    {
        get { return m_pInfoPrefab; }
        set { m_pInfoPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedInfo { get; private set; }

    void Start()
    {
        Button btn1 = destination1.GetComponent<Button>();
        Button btn2 = destination2.GetComponent<Button>();
        Button btn3 = destination3.GetComponent<Button>();
        Button btn4 = destination4.GetComponent<Button>();

        btn1.onClick.AddListener(TaskOnClick);
        btn2.onClick.AddListener(TaskOnClick);
        btn3.onClick.AddListener(TaskOnClick);
        btn4.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        Debug.Log("onClick");
        spawnedInfo = Instantiate(m_pInfoPrefab);
        spawnedInfo.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        m_connection.Arnavi = spawnedInfo;
        m_connection.SetBtn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
