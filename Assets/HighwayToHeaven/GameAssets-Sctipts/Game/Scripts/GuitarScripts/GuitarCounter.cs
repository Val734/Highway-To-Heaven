using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//En este código incremento el contador y actualizo el texto en pantalla cuando recoge una guitarra. 

public class GuitarCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    public int pickedGuitars;
    public GuitarBar guitarBar;


    public void CounterForGuitarsTextUpdater()
    {
        counterText.text = $" Guitars collected:{pickedGuitars}";
        guitarBar = FindObjectOfType<GuitarBar>();

    }

    public void UpdateGuitarCounter()
    {
        pickedGuitars += 1;
        CounterForGuitarsTextUpdater();

        if (guitarBar != null)
        {
            guitarBar.CollectGuitar();
        }
    }
}




 