using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;



public class GoogleMap : MonoBehaviour
{
    public RawImage mapRawImage;
    string url;
    public float lat;
    public float lon;
    public float destLat = 37.494389f;
    public float destLon= 126.957647f;

    public int zoom = 18;   
    public int mapWidth = 600;                // 무료 지원 최대 해상도
    public int mapHeight = 600;
    
    public enum mapType {roadmap, satellite, hybrid, terrain};
    public mapType mapSelected;
    public int scale;    
    
    static float UPDATE_TIME = 3f;
    float updateTime = UPDATE_TIME;
    
    IEnumerator Map(){
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
                "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale + "&maptype="
                + mapSelected + "&key=AIzaSyCcjI0eR4mH1rMCxosA1aSdEh9CwqoXzCM" + 
                "&markers=color:red%7Clabel:C%7C" + lat + "," + lon +
                "&markers=color:green%7Clabel:C%7C" + destLat + "," + destLon;
        WWW www = new WWW(url);
        yield return www;
        mapRawImage.texture = www.texture;        
    }          
    
    public void Start()
    {
        
    }
    public void Update()
    {
        StartCoroutine(startGPS());
        mapRawImage = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
        if(updateTime>=0){
            updateTime = updateTime - Time.deltaTime;
        }else{
            updateTime = UPDATE_TIME;
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            if(latitude!=0f&&longitude!=0f){
                lat = latitude;
                lon = longitude;
                StartCoroutine(Map());
            }
        }
    }

    IEnumerator startGPS(){
        if(!Input.location.isEnabledByUser){
            yield break;
        }
        Input.location.Start();

        int maxWait = 20;
        while(Input.location.status==LocationServiceStatus.Initializing&&
        maxWait>0){
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if(maxWait<1){
            print("Timed Out");
            yield break;
        }
        if(Input.location.status==LocationServiceStatus.Failed){
            print("Unable to determine device location");
            yield break;
        }else{
            print("Location: " + Input.location.lastData.latitude
            + " " + Input.location.lastData.longitude + " "
            + Input.location.lastData.altitude + " " + 
            + Input.location.lastData.horizontalAccuracy + " "
            + Input.location.lastData.timestamp);
        }
    }
   
}



