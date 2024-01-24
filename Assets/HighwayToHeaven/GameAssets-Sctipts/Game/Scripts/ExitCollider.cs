using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    // Creo el tiiempo antes de destruir el bloque después de la colisión

    [SerializeField] float timerForBlocks = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        //Cuando se detecta una colisión con otro collider destruye el objeto padre del bloque después de un tiempo determinado
        //También encuentra el objeto con  la etiqueta de blockmanager en la escena y llama al método OnDestroyBlock() en el componente BlockGeneratir del objeto BlockManager
        Destroy(transform.parent.gameObject, timerForBlocks);
        GameObject BlockManager = GameObject.FindGameObjectWithTag("BlockManager");
        BlockManager.GetComponent<BlockGenerator>().OnDestroyBlock(); 
    }
}
