using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallMovement : MonoBehaviour
{
    private Handforce Handf;
    public float speed = 0.5f;
    public float jump = 5f;
    public GameObject stick;
    public GameObject goal;
    private Rigidbody ballrb;
    public bool ballmove;
    private WinController Winct;
    private int wcount;
    public AudioSource audioSource;
    public AudioClip ballSMaxSound;
    public AudioClip ballMaxSound;
    public AudioClip ballSound;



    // Start is called before the first frame update

    void Start() {

            Handf = FindObjectOfType<Handforce>();
        ballrb = GetComponent<Rigidbody>();
        Winct = FindObjectOfType<WinController>();
        // ballstart = (int)gameObject.transform.rotation.x;
        ballmove = false;

    }
    private void Update()
    {

        if (Handf.finish == true&&!ballmove)
        {
            if (!ballmove)
            {
                Winct.wincount--;
                PlayerPrefs.SetInt("Wincount", Winct.wincount);
            }
            Handf.finish = false;
            
        }
    }
 
    public void Playball(float force)
    {
        Handf.finish = true;
        Handf.touch = true;
        if (force == 100)
        {
            audioSource.clip=ballSMaxSound;
            audioSource.Play();
        }
        else if (force > 70)
        {
            audioSource.clip = ballMaxSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = ballSound;
            audioSource.Play();
        }
        ballrb.AddForce(0, force*jump, force * speed);
        //ballrb.AddForce( transform.forward * force*speed);
    

    }
   /* public IEnumerator Losing()
    {
     
        wcount =PlayerPrefs.GetInt("Wincount");
        Debug.Log(transform.position.z);
        if (transform.position.z > goal.transform.position.z + 1f )
        {
            Debug.Log("이게왜안되지? ");
            if (wcount > 0)
            {

                wcount--;
                PlayerPrefs.SetInt("Wincount", wcount);
                Debug.Log("loosing = " + wcount);
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               
            }

        }


        yield return new WaitForSeconds(6f);
        if (transform.position.z < goal.transform.position.z )
        {
            if (wcount > 0)
            {

                wcount--;
                PlayerPrefs.SetInt("Wincount", wcount);
                Debug.Log("loosing2 = " + wcount);
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               
            }
 
            
        }
      if (wcount == 0)
        {
            Debug.Log(wcount);
            PlayerPrefs.SetInt("Wincount", 0);
        }


    }*/

}
