using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerClick : MonoBehaviour, IPointerClickHandler
{
    SpriteRenderer sprite;
    public Color target = Color.green;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        Debug.Log("marker clicked");
        if (sprite)
        { sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
