
using UnityEngine;
using UnityEngine.Events;

public class GuitarBehaviour : MonoBehaviour
{

    //Este script se ejecuta cuando el jugador recoge una guitarra actualizando el contador, destruyendo la guitarra y se activa la animación
    static public int count;
    public UnityEvent PickedGuitar;
    private bool isPicked = false;
    private Animator _anim;
    public GameObject guitarManager;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        guitarManager = GameObject.FindGameObjectWithTag("Guitars_manager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPicked && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("guitarrasooooo");
            PickedGuitar.Invoke();
            isPicked = true;
            _anim.SetTrigger("PickGuitar");
            guitarManager.GetComponent<GuitarCounter>().UpdateGuitarCounter();
            Destroy(gameObject, 0.5f);
        }


    }
}

