using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference Pause;
    public GameObject options_menu;


    private void Start()
    {
        options_menu.SetActive(false);
        Time.timeScale = 1f;    

    }
    private void OnEnable()
    {
        Pause.action.Enable();
        Pause.action.performed += OpenPauseMenu;
    }

    private void OnDisable()
    {
        Pause.action.Disable();
        Pause.action.performed -= OpenPauseMenu;

    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (!options_menu.activeSelf)
        {
            options_menu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            options_menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ClosePauseMenu()
    {
        options_menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestartGame()
    {
        Debug.Log("aqui reseteas");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
