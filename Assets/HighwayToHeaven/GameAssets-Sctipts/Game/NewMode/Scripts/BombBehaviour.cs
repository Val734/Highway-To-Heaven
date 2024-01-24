using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class BombBehaviour : MonoBehaviour
{
    public UnityEvent PickedBomb;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }


    private bool isPicked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPicked && other.gameObject.CompareTag("Player"))
        {
            PickedBomb.Invoke();
            isPicked = true;

            // Agrega la lógica para recolectar la bomba aquí, por ejemplo, actualizar el contador en BombUIUpdater.
            BombUIUpdater bombUIUpdater = FindObjectOfType<BombUIUpdater>();
            if (bombUIUpdater != null)
            {
                bombUIUpdater.CollectBomb();
            }
            _anim.SetTrigger("PickBomb");
            Destroy(gameObject, 0.5f);
        }
    }
}
