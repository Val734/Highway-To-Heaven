using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScrenCheckMarck : MonoBehaviour
{
    [SerializeField] Toggle tg; 
    void Start()
    {
        if (tg.isOn)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
        
    }
    public void activateFullScreen(bool fullScreen)
    {
        Screen.fullScreen=fullScreen; 
    }
}
