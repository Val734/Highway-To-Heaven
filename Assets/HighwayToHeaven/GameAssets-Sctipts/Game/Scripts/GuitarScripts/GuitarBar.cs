using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GuitarBar : MonoBehaviour
{

    //Este script gestion la recolección de guitarras en el juego y muestra el progreso a través de una slidebar 
    [SerializeField] InputActionReference guitarbar;

    public Slider progressBar;
    public GameObject button;
    public int maxGuitars = 10;
    private int collectedGuitars = 0;
    bool butonactive;

    private void OnEnable()
    {
        guitarbar.action.Enable();
        guitarbar.action.performed += activateInput => OnGuitarAction();
    }

    private void OnDisable()
    {
        guitarbar.action.Disable();
        guitarbar.action.performed -= activateInput => OnGuitarAction();
    }

    public void CollectGuitar()
    {
        if (butonactive == false)
        {
            collectedGuitars++;

            float fillValue = (float)collectedGuitars / maxGuitars;
            progressBar.value = fillValue;
        }

        if (collectedGuitars >= maxGuitars)
        {
            fullbar();
        }
    }

    public void fullbar()
    {
        butonactive = true;

        if (button != null)
        {
            button.SetActive(true);
        }
    }

    private void OnGuitarAction()
    {
        if (butonactive)
        {
            ActivateGuitarBar();
        }
    }

    public void ActivateGuitarBar()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerBehaviour>().ActivateSpeedBar();
        button.SetActive(false);
        butonactive = false;
        ResetProgressBar();
    }

    public void ResetProgressBar()
    {
        collectedGuitars = 0;
        progressBar.value = 0f;
        butonactive = false;
    }
}
