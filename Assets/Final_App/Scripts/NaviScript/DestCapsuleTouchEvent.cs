using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private string thisObj;
    void Start()
    {
        thisObj=this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene(2);
    }
}
