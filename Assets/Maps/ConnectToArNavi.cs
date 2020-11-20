using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToArNavi : MonoBehaviour
{
    public GameObject Arnavi;

    void Start()
    {
        
    }
    public void SetBtn()
    {
        Button btn = Arnavi.GetComponent<Button>();
        btn.onClick.AddListener(startNavi);
    }
    public void startNavi() //startscene->gamescene
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
