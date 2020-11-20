using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public void OnClickArrow()   // Arrow prefab 불러오기
    {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Arrow")) as GameObject;
    }
}
