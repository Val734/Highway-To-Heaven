using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTextUpdater : MonoBehaviour
{
    public void SetTimeText(float time)
    {
        //Esto es para convertir el los segundos a minutos
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        GetComponent<TMP_Text>().text = "Time Elapsed: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
