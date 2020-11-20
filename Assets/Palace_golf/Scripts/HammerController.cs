using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    float ballforce;
    private BallMovement Ballmv;


    // Start is called before the first frame update
    void Start()
    {
        Ballmv = FindObjectOfType<BallMovement>();

    }

   
    public void ballready(float a) {
        ballforce = a;
      
    }
   /* void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ball")
        {g
            Ballmv.ballmove = true;
            Ballmv.Playball(ballforce);

        }
    }*/
    void OnCollisionEnter(Collision col)
    {

        if (col.collider.tag == "Ball")
        {
            Ballmv.ballmove = true;
            Ballmv.Playball(ballforce);

        }
    }



}
