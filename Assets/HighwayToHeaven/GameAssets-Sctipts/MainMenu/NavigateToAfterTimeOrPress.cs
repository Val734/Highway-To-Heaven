
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NavigateToAfterTimeOrPress : MonoBehaviour
{
    public string scene; 
    public void NavigateToNextScene()
    {
       SceneManager.LoadScene(scene);
    }


}
