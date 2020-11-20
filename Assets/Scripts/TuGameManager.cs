using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuGameManager : MonoBehaviour
{
    public int Score;
    public TMP_Text ScoreText;

    void Update()
    {
        //score가 5가 되면 종료
        if (Score >= 5)
        {
            Clear();
        }
    }

    void Clear()    //CLEAR 나타내고 메인화면으로 scene 전환(메인화면으로 scene전환하는 건 게임 다 합치고 나서 넣을 예정)
    {
        ScoreText.text = "clear";
        Time.timeScale = 0f;
    }

    //score 증가시키고, 증가시킨 score을 text로 나타냄
    public void AddScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }

}
