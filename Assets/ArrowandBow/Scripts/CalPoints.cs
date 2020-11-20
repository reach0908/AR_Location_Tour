using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalPoints : MonoBehaviour
{

    public Vector3 Center;
    private int Score;
    public KeepScore m_ScoreText;

    Rigidbody Arrow_rb;

    private void Start()
    {
        Center = GameObject.Find("NewCenter(Clone)").transform.position;
        m_ScoreText = GameObject.FindObjectOfType<KeepScore>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log("Hit the target");
            Arrow_rb = collision.gameObject.GetComponent<Rigidbody>();
            Arrow_rb.useGravity = false;
            Arrow_rb.isKinematic = true;

            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;
           
            double radius = Math.Sqrt(Math.Pow(position.x - Center.x, 2) + Math.Pow((position.y - Center.y), 2));

            if (radius <= 3)
            {
                Score = (int)((3 - radius) / (0.3));
                Debug.Log(Score);
                KeepScore.Score += Score;
                m_ScoreText.setScore();
            }
            //Destroy Arrow
            

        }

    }
}
