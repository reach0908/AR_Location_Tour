using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public GameObject stopcv;
    public GameObject Topbar;
    
    // public GameObject exppcv;
    private bool pause=false;
    public AudioSource btnclickSound;
    private float fixedDeltatime;

    private void Awake()
    {
        {
            this.fixedDeltatime = Time.fixedDeltaTime;
        }
    }
    void Update()
    {
        if (pause)
        {
            Time.timeScale = 0;
            
            Topbar.SetActive(false);
            stopcv.SetActive(true);
           
        }

    }
    public void startBtn() //startscene->gamescene
    {
       //팝업 버튼에 따라 시작 할 게임을 달리해야..? 우선 무조건 g_Scene으로 가게 할게요.
        SceneManager.LoadScene("Path");
    }
    public void restartBtn() //=okbtn 각 특정점수제거하면서 새로시작
    {
        btnclickSound.Play();
        if (Time.timeScale == 0)
        {
            stopcv.SetActive(false);
            Topbar.SetActive(true);
            Time.timeScale = 1f;
            
            pause = false;

        }
        Time.fixedDeltaTime = this.fixedDeltatime * Time.timeScale;
        if (SceneManager.GetActiveScene().name == "Golf") {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("g_Wincount"); //기회
            PlayerPrefs.DeleteKey("golfGame");
            SceneManager.LoadScene("Golf");
        }
        if (SceneManager.GetActiveScene().name == "Dice")
        {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("d_Wincount"); //기회
            PlayerPrefs.DeleteKey("diceGame");
            SceneManager.LoadScene("Dice");
        }
        if (SceneManager.GetActiveScene().name == "Tuho")
        {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("t_Wincount"); //기회
            PlayerPrefs.DeleteKey("tuhoGame");
            SceneManager.LoadScene("Tuho");
        }
        if (SceneManager.GetActiveScene().name == "Arrow")
        {
            PlayerPrefs.DeleteKey("arrowGame");
            KeepScore.Score = 0; // text 바뀌는지
            Controller.chances = 4;
            Controller.isNotched = false;
            SceneManager.LoadScene("Arrow");
            
        }

    }
    public void quit_restartBtn() // 아예 처음부터
    {
        btnclickSound.Play();
        if (SceneManager.GetActiveScene().name == "Golf")
        {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("g_Wincount"); //기회
            PlayerPrefs.DeleteKey("golfGame");
            SceneManager.LoadScene("Golf");
        }
        if (SceneManager.GetActiveScene().name == "Dice")
        {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("d_Wincount"); //기회
            PlayerPrefs.DeleteKey("diceGame");
            SceneManager.LoadScene("Dice");
        }
        if (SceneManager.GetActiveScene().name == "Tuho")
        {
            PlayerPrefs.DeleteKey("Ground");
            PlayerPrefs.DeleteKey("t_Wincount"); //기회
            PlayerPrefs.DeleteKey("tuhoGame");
            SceneManager.LoadScene("Tuho");
        }
        if (SceneManager.GetActiveScene().name == "Arrow")
        {
            PlayerPrefs.DeleteKey("arrowGame");
            KeepScore.Score = 0; // text 바뀌는지
            Controller.chances = 4;
            Controller.isNotched = false;
            SceneManager.LoadScene("Arrow");
        }
       
    }
 

    public void OkBtn() //지도화면 , 
    {
        btnclickSound.Play();
        if (SceneManager.GetActiveScene().name == "Golf")
        {
            if ((PlayerPrefs.HasKey("golfWinScore")))
            {
                SceneManager.LoadScene(1);
            }
            if (!(PlayerPrefs.HasKey("golfWinScore")))
            {
               /*여기 성공한 값 없이 그냥 지도로 나가게 되다는 ㄴ것을
                * 좀 더 보기 좋게 정리하려고 필요없는 데 만들었습니다.
                * 삭제 해도 상관없을 것 같습니다.

                */
                SceneManager.LoadScene(1);
            }
        }
        if (SceneManager.GetActiveScene().name == "Dice")
        {

          if ((PlayerPrefs.HasKey("diceWinScore")))
            {
                SceneManager.LoadScene(1);
            }
            if (!(PlayerPrefs.HasKey("diceWinScore")))
            {
               /*여기 성공한 값 없이 그냥 지도로 나가게 되다는 ㄴ것을
                * 좀 더 보기 좋게 정리하려고 필요없는 데 만들었습니다.
                * 삭제 해도 상관없을 것 같습니다.

                */
                SceneManager.LoadScene(1);
            }
        }
        if (SceneManager.GetActiveScene().name == "Tuho")
        {
            if ((PlayerPrefs.HasKey("tuhoWinScore")))
            {
                SceneManager.LoadScene(1);
            }
            if (!(PlayerPrefs.HasKey("tuhoWinScore")))
            {
               /*여기 성공한 값 없이 그냥 지도로 나가게 되다는 ㄴ것을
                * 좀 더 보기 좋게 정리하려고 필요없는 데 만들었습니다.
                * 삭제 해도 상관없을 것 같습니다.

                */
                SceneManager.LoadScene(1);
            }
        }
        if (SceneManager.GetActiveScene().name == "Arrow")
        {
            if ((PlayerPrefs.HasKey("arrowWinScore")))
            {
                SceneManager.LoadScene(1);
            }
            if (!(PlayerPrefs.HasKey("arrowWinScore")))
            {
               /*여기 성공한 값 없이 그냥 지도로 나가게 되다는 ㄴ것을
                * 좀 더 보기 좋게 정리하려고 필요없는 데 만들었습니다.
                * 삭제 해도 상관없을 것 같습니다.

                */
                SceneManager.LoadScene(1);
            }
        }


    }
    public void StopBtn()
    {
       
        btnclickSound.Play();
        pause = true;

      
        if (Time.timeScale == 0)
        {
            stopcv.SetActive(false);
            Topbar.SetActive(true);
            Time.timeScale = 1f;
            
            pause = false;
            
        }
        Time.fixedDeltaTime = this.fixedDeltatime * Time.timeScale;
    }
}
