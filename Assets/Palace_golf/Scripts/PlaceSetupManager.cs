using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceSetupManager: MonoBehaviour
{
    public bool setposition;

    void Start()
    {
        setposition=false;
    }
   
    void Update() { 
        if ((PlayerPrefs.HasKey("golfposition_x"))&&!setposition)
        {
            float golf_x=PlayerPrefs.GetFloat("golfposition_x");
            float golf_y = PlayerPrefs.GetFloat("golfposition_z");
            float golf_z = PlayerPrefs.GetFloat("golfposition_z");
            float R_golf_x = PlayerPrefs.GetFloat("golfrotation_x");
            float R_golf_y = PlayerPrefs.GetFloat("golfrotation_y");
            float R_golf_z = PlayerPrefs.GetFloat("golfrotation_z");

            gameObject.transform.position = new Vector3(golf_x, golf_y, golf_z);
            gameObject.transform.rotation = new Quaternion();
            gameObject.transform.rotation = Quaternion.Euler(R_golf_x, R_golf_y, R_golf_z);
            setposition = true;
        }
    }
    public void SettingUp()
    {
     
            gameObject.SetActive(true);
        
    }
 
  
}
