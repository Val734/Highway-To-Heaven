using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks;
    int randomBlock;
    Vector3 lastPosition = new Vector3(0, 0, 0);
    [SerializeField] int maxBlocks;
    public int SpawnedBlocks;
    public GameObject firstBlock;

    void Update()
    {
        while (SpawnedBlocks < maxBlocks)
        {
            if (SpawnedBlocks == 0)
            {
                GameObject firstBlockInstance = Instantiate(firstBlock, lastPosition, Quaternion.identity);
                Transform lastPieceOfTheFirstBlock = firstBlockInstance.GetComponentInChildren<ExitCollider>().transform;
                lastPosition = lastPieceOfTheFirstBlock.position + Vector3.right;

                // Hacer que el primer bloque sea hijo del GameObject que tiene este script
                firstBlockInstance.transform.parent = transform;

                SpawnedBlocks++;
            }
            else
            {
                randomBlock = Random.Range(0, blocks.Length);
                GameObject blockInstance = Instantiate(blocks[randomBlock], lastPosition, Quaternion.identity);

                Transform lastPiece = blockInstance.GetComponentInChildren<ExitCollider>().transform;
                lastPosition = lastPiece.position + Vector3.right;

                // Hacer que el bloque instanciado sea hijo del GameObject que tiene este script
                blockInstance.transform.parent = transform;

                SpawnedBlocks++;
            }
        }
    }

    public void OnDestroyBlock()
    {
        SpawnedBlocks--;
    }
}
