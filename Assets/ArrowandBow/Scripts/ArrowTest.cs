using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 prevPos;

    // Update is called once per frame
    void Update()
    {

        Debug.Log(" Arrow Pos " + transform.localPosition);

        if (transform.localPosition != prevPos)
            Debug.Log("value changed");

        prevPos = transform.localPosition;
    }
}
