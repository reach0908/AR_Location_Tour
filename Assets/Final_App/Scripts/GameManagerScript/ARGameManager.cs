using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class ARGameManager : MonoBehaviour
{
    public static ARGameManager instance;
    public GameObject Wincv;
    public GameObject One_Wincv;
    public GameObject Losecv;
    public GameObject[] heartscore;
    int golfscore = 5;
    int dicescore = 5;
    int tuhoscore = 5;
    int arrowscore = 5;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
     * 이 양식으로 해주셔야 실행됩니다.
     * 게임이 이기든 지든 메소드안의 코드에ex)PlayerPrefs.SetInt("golfGame", 1234);를 저장하시고 1234 아니여도됨
      몇번째 이겼는지의 특정 변수에다가 저장되어 있는 값을 ex)PlayerPrefs.SetInt("golfWinScore",변수이름);해주세요. 그다음
      마지막에 GameManager.instance.Game_resultcv();을 쓰시면 이 메소드가 호출되고 Game_resultcv()가 실행
     진 상황의 값dmf 0으로 해서 저장 후 똑같이 호출해 주세요 . .
      */
    public void Game_resultcv()
    {

        if (SceneManager.GetActiveScene().name == "Golf")
        {
            if ((PlayerPrefs.HasKey("golfGame")))
            {
                if ((PlayerPrefs.HasKey("golfWinScore")))
                {
                    golfscore = PlayerPrefs.GetInt("golfWinScore");


                    if (golfscore == 0)
                    {

                        Losecv.SetActive(true);
                  
                    }

                    else if (golfscore == 1)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                    
                        Wincv.SetActive(true);
                        StartCoroutine(Scoreanimation(1));

                    }
                    else if (golfscore == 2)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);
                        StartCoroutine(Scoreanimation(2));

                    }
                    else if (golfscore == 3)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);
                        StartCoroutine(Scoreanimation(3));

                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Dice")
        {
            if ((PlayerPrefs.HasKey("diceGame")))
            {
                if ((PlayerPrefs.HasKey("diceWinScore")))
                {
                    dicescore = PlayerPrefs.GetInt("diceWinScore");


                    if (dicescore == 0)
                    {

                        Losecv.SetActive(true);
                    }

                    else if (dicescore == 1)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);

                        Wincv.SetActive(true);

                        StartCoroutine(Scoreanimation(1));

                    }
                    else if (dicescore == 2)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);

                        StartCoroutine(Scoreanimation(2));

                    }
                    else if (dicescore == 3)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);

                        StartCoroutine(Scoreanimation(3));

                    }
                }

            }
        }
        if (SceneManager.GetActiveScene().name == "Tuho")
        {

            if ((PlayerPrefs.HasKey("tohuGame")))
            {
                if ((PlayerPrefs.HasKey("tuhoWinScore")))
                {
                    tuhoscore = PlayerPrefs.GetInt("tuhoWinScore");


                    if (tuhoscore == 0)
                    {

                        Losecv.SetActive(true);
                    }

                    else if (tuhoscore == 10)
                    {
                        One_Wincv.SetActive(true);
                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Arrow")
        {

            if ((PlayerPrefs.HasKey("arrowGame")))
            {
                if ((PlayerPrefs.HasKey("arrowWinScore")))
                {
                    arrowscore = PlayerPrefs.GetInt("arrowWinScore");


                    if (arrowscore == 0)
                    {

                        Losecv.SetActive(true);
                    }

                    else if (arrowscore == 1)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);

                        Wincv.SetActive(true);

                        StartCoroutine(Scoreanimation(1));

                    }
                    else if (arrowscore == 2)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);

                        StartCoroutine(Scoreanimation(2));

                    }
                    else if (arrowscore == 3)
                    {
                        heartscore[2].SetActive(false);
                        heartscore[1].SetActive(false);
                        heartscore[0].SetActive(false);
                        Wincv.SetActive(true);
                        StartCoroutine(Scoreanimation(3));

                    }
                }
            }
        }
    }

                private IEnumerator Scoreanimation(int i)
                {

                    if (i == 1)
                    {
                        yield return new WaitForSeconds(0.7f);
                        heartscore[0].SetActive(true);
                    }
                    if (i == 2)
                    {
                        yield return new WaitForSeconds(0.7f);
                        heartscore[0].SetActive(true);
                        yield return new WaitForSeconds(0.3f);
                        heartscore[1].SetActive(true);
                    }
                    if (i == 3)
                    {
                        yield return new WaitForSeconds(0.7f);
                        heartscore[0].SetActive(true);
                        yield return new WaitForSeconds(0.3f);
                        heartscore[1].SetActive(true);
                        yield return new WaitForSeconds(0.3f);
                        heartscore[2].SetActive(true);
                    }

                }
}
    
