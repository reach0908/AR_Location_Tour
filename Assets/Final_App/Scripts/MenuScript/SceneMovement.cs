using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private string thisObj;
    void Start()
    {
        thisObj = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene(1);
        }
    }

    
}
