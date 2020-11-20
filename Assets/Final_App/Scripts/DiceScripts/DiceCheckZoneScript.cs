using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 diceVelocity;
	public GameObject playerHorse = null;
    GameObject boardchecker;
    private int curScore;
    private int playerScore;
    public int chance;
	public int wincount;
	public bool winset = false;
	void Start()
    {
        boardchecker = GameObject.Find("Board").transform.GetChild(0).gameObject;
        playerHorse.transform.position = boardchecker.transform.position;
		//현재 주사위 점수
        curScore=0;
		// 찬스 내 총합 점수
		playerScore=0;
		
        chance = 3;
		winset = false;
		if ((PlayerPrefs.HasKey("d_Wincount")))
        {
           wincount= PlayerPrefs.GetInt("d_Wincount");
        }
        if (!(PlayerPrefs.HasKey("d_Wincount")))
        {
            PlayerPrefs.SetInt("d_Wincount", 3);
            wincount = PlayerPrefs.GetInt("d_Wincount");
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
		diceVelocity = DiceScript.diceVelocity;
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log(col);
		
			Debug.Log("for문진입");
            
			switch (col.gameObject.name) {
			case "Side1":
				DiceNumberTextScript.diceNumber = 1;
				curScore=1;
				break;
			case "Side2":
				DiceNumberTextScript.diceNumber = 2;
				curScore=2;
				break;
			case "Side3":
				DiceNumberTextScript.diceNumber = 3;
				curScore=3;
				break;
			case "Side4":
				DiceNumberTextScript.diceNumber = 4;
				curScore=4;
				break;
			case "Side5":
				DiceNumberTextScript.diceNumber = 5;
				curScore=5;
				break;
			case "Side6":
				DiceNumberTextScript.diceNumber = 6;
				curScore=6;
				break;
			}
			if(playerScore<=18&&chance>0&&DiceScript.isCnt){
				
				playerScore=curScore+playerScore;
				boardchecker = GameObject.Find("Board").transform.GetChild(playerScore).gameObject;
            	playerHorse.transform.position = boardchecker.transform.position;
            	chance--;
				if(chance==0){
					StartCoroutine(Losing());
				}
				ChanceNumberTextScript.chanceNumber = chance;
				DiceScript.isCnt=false;
			}
	}
	public IEnumerator Losing()
    {
		yield return new WaitForSeconds(1f);
		if(playerScore>15){
			wincount=3;
		}else if(playerScore>12){
			wincount=2;
		}else if(playerScore>8){
			wincount=1;
		}else{
			wincount=0;
		}
        PlayerPrefs.SetInt("diceGame", 1234);
        if (wincount == 3)
        {
            PlayerPrefs.SetInt("diceWinScore", wincount);  
        	ARGameManager.instance.Game_resultcv();
        }
        else if (wincount == 2)
        {
            PlayerPrefs.SetInt("diceWinScore", wincount);         
            ARGameManager.instance.Game_resultcv();
        }
        else if (wincount == 1)
        {
            PlayerPrefs.SetInt("diceWinScore", wincount);
             ARGameManager.instance.Game_resultcv();
         }else{
			PlayerPrefs.SetInt("diceWinScore", wincount);
            ARGameManager.instance.Game_resultcv();
		}
    }
}
