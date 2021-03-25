﻿using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour , IPointerUpHandler,IPointerDownHandler
{
    [HideInInspector]
    public bool pressedJoyButton;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressedJoyButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressedJoyButton = false;
    }
}
