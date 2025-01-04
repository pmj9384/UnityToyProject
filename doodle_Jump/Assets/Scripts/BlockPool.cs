using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 블록 프리팹 배열
    public int poolSize; // Pool 크기

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {

        // Pool 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject block = InstantiateRandomBlock();
            block.SetActive(false);
            pool.Enqueue(block);
        }
    }

    private GameObject InstantiateRandomBlock()
    {
        int randomIndex = Random.Range(0, blockPrefabs.Length);
        return Instantiate(blockPrefabs[randomIndex]);
    }

    public GameObject GetBlock()
    {
        if (pool.Count > 0)
        {
            GameObject block = pool.Dequeue();
            block.SetActive(true);
            return block;
        }
        else
        {
        
        return InstantiateRandomBlock();  // 반환됨
        }
    }

    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        pool.Enqueue(block);
    }
}
