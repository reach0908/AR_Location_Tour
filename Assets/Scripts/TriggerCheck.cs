using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public TuGameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        gameManager.AddScore();
        Destroy(other.gameObject);
    }

}
