using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public GameObject g_slider;
    public GameObject ball;
    private Handforce Handf;
    public int wincount=3;
    public bool winset = false;
    public Text scoretext;
    public GameObject button;
 
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Golf")
        { button.SetActive(true); }
            winset = false;
        Handf = FindObjectOfType<Handforce>();

        if ((PlayerPrefs.HasKey("g_Wincount")))
        {
           wincount= PlayerPrefs.GetInt("g_Wincount");


        }
        if (!(PlayerPrefs.HasKey("g_Wincount")))
        {
           
            wincount = 3;
        }
        scoretext.text = "기회" + wincount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Handf.finish == true && Handf.touch == true)
        {
            StartCoroutine(Losing());
        }
   

    }
    void OnCollisionStay(Collision col)
    {
        button.SetActive(false);
        g_slider.SetActive(false);
        if (col.collider.tag == "Ball")
        {
            PlayerPrefs.SetInt("golfGame", 1234);
            if (wincount == 3)
            {
            
                PlayerPrefs.SetInt("golfWinScore", wincount);
                ARGameManager.instance.Game_resultcv();
            }
            else if (wincount == 2)
            {
                PlayerPrefs.SetInt("golfWinScore", wincount);              
                ARGameManager.instance.Game_resultcv();
            }
            else if (wincount == 1)
            {
                PlayerPrefs.SetInt("golfWinScore", wincount);
                ARGameManager.instance.Game_resultcv();
            }

        }
    }


    public IEnumerator Losing()
    {

        yield return new WaitForSeconds(1f);
        if (wincount == 0)
        {
            button.SetActive(false);

            PlayerPrefs.SetInt("golfGame", 1234);
            PlayerPrefs.SetInt("golfWinScore", 0);
            g_slider.SetActive(false);
            ARGameManager.instance.Game_resultcv();
        }
        if (ball.transform.position.z > gameObject.transform.position.z + 1f)
        {
            
            if (wincount>0&&!winset)
            {
                wincount--;
                PlayerPrefs.SetInt("g_Wincount", wincount);
                winset = true;

                
                yield return new WaitForSeconds(5f);
                scoretext.text = "다시시작중";
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
              

            }
        }


        if (ball.transform.position.z < gameObject.transform.position.z)
        {
            yield return new WaitForSeconds(5f);
            if (wincount >0 && !winset)
            {

                wincount--;
                PlayerPrefs.SetInt("g_Wincount", wincount);
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                winset = true;

                yield return new WaitForSeconds(5f);
                scoretext.text = "다시시작중";
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
         

        }

    }
}
        


    
