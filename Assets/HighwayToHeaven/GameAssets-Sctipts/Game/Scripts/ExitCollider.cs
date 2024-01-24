using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    // Creo el tiiempo antes de destruir el bloque despu�s de la colisi�n

    [SerializeField] float timerForBlocks = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        //Cuando se detecta una colisi�n con otro collider destruye el objeto padre del bloque despu�s de un tiempo determinado
        //Tambi�n encuentra el objeto con  la etiqueta de blockmanager en la escena y llama al m�todo OnDestroyBlock() en el componente BlockGeneratir del objeto BlockManager
        Destroy(transform.parent.gameObject, timerForBlocks);
        GameObject BlockManager = GameObject.FindGameObjectWithTag("BlockManager");
        BlockManager.GetComponent<BlockGenerator>().OnDestroyBlock(); 
    }
}
