using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private float ground;
    private float losingground;
    // Start is called before the first frame update
    void Start()
    {
        ground = Random.Range(0f, 1f);

       
        CreateGround(ground);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateGround(float intensity)
    {
        if (!(PlayerPrefs.HasKey("Ground")))
        {
            
            float groundposition = Mathf.Lerp(-1f, 1.5f, intensity);
            gameObject.transform.position = new Vector3(0, 0.0f, groundposition);
            PlayerPrefs.SetFloat("Ground", intensity);
        }
        else if(PlayerPrefs.HasKey("Ground"))
        {
            
            losingground = PlayerPrefs.GetFloat("Ground");
            float groundposition = Mathf.Lerp(-1f,1.5f, losingground);
            gameObject.transform.position = new Vector3(0, 0.0f, groundposition);
        }
    }
}
