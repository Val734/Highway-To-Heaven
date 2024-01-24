using System.Collections;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject xBackgroundPrefab;
    public GameObject zBackgroundPrefab;
    Vector3 lastPosition = new Vector3(0, 0, 0);
    [SerializeField] int maxBlocks;
    public int spawnedBlocks;
    public float xSpacing = 30.35205f;
    public float zSpacing = -28f;
    public float timeBetweenDestruction = 10f;
    private bool generateInXDirection = true;

    private void Update()
    {
        if (spawnedBlocks < maxBlocks)
        {
            GameObject backgroundPrefab;

            if (generateInXDirection)
            {
                backgroundPrefab = xBackgroundPrefab;
            }
            else
            {
                backgroundPrefab = zBackgroundPrefab;
            }

            GameObject background = Instantiate(backgroundPrefab, lastPosition, Quaternion.identity);
            ExitCollider exitCollider = background.GetComponentInChildren<ExitCollider>();
            Vector3 nextPosition;

            if (generateInXDirection)
            {
                nextPosition = exitCollider.transform.position + new Vector3(xSpacing, 0f, 0f);
            }
            else
            {
                nextPosition = exitCollider.transform.position + new Vector3(0f, 0f, zSpacing);
            }

            lastPosition = nextPosition;
            spawnedBlocks++;

            // Hacer que el bloque instanciado sea hijo del GameObject que tiene este script
            background.transform.parent = transform;

            StartCoroutine(DestroyBlockAfterTime(background, timeBetweenDestruction * spawnedBlocks));

            generateInXDirection = !generateInXDirection;
        }
    }

    private IEnumerator DestroyBlockAfterTime(GameObject block, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(block);
        spawnedBlocks--;
    }
}
