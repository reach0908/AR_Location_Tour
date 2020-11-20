using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{
    public Text scoreText;

    //public Controller controller;
    public static int Score = 0;
    private int savedScore;
    private string KeyString = "ArrowScore";

    private void Start()
    {
        savedScore = PlayerPrefs.GetInt(KeyString, 0);
        scoreText = GetComponent<Text>();
        //Arrow = GameObject.FindGameObjectWithTag("Arrow");
        //Center = GameObject.Find("Center").transform.position;
    }
   
    private void Update()
    {
        //setScore();
    }

    public void setScore()
    {
        Debug.Log(" Score :" + scoreText.text, gameObject);
        scoreText.text = Score.ToString("0");
    }
    
}
