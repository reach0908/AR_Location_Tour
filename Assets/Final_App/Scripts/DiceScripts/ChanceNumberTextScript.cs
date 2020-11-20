using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceNumberTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
	public static int chanceNumber;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "남은 기회 :" + chanceNumber.ToString ();
	}
}
