using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public Transform player; 
    public BlockPool blockPool;
    public float blockHeight = 8f; 
    public float minBlockHeight = 25f; 
    public int visibleBlockCount; 

    private float lastBlockY = 0f; 
    private Queue<GameObject> activeBlocks = new Queue<GameObject>(); // 활성 블록 큐

    private void Start()
    {
        if (blockPool == null)
        {
            blockPool = FindObjectOfType<BlockPool>();

        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }

        }

        for (int i = 0; i < visibleBlockCount; i++)
        {
            CreateBlock();
        }
    }

    private void Update()
    {
        if (player == null || blockPool == null)
        {
            return;
        }

        if (player.position.y > lastBlockY - (visibleBlockCount * blockHeight / 1.5f))
        {
            if (activeBlocks.Count >= visibleBlockCount)
            {
                RemoveBlock();
            }
            CreateBlock();
        }

        // 블록 삭제 조건: 플레이어가 특정 높이 이상 올라갔을 때만 삭제
        if (activeBlocks.Count > 0 && player.position.y - activeBlocks.Peek().transform.position.y > visibleBlockCount * blockHeight * 1.5f)
        {
            RemoveBlock();
        }
    }

    private void CreateBlock()
    {
        GameObject block = blockPool.GetBlock();

        float randomX = Random.Range(-50f, 50f); 
        float randomZ = Random.Range(-50f, 50f);


        float newBlockY = lastBlockY + Random.Range(minBlockHeight, blockHeight); 

        block.transform.position = new Vector3(randomX, newBlockY, randomZ);
        activeBlocks.Enqueue(block);

        lastBlockY = newBlockY; 
    }

    private void RemoveBlock()
    {
        if (activeBlocks.Count > 0)
        {
            GameObject block = activeBlocks.Dequeue();
            blockPool.ReturnBlock(block);
        }
    }
}
