using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public BlockPool blockPool; // BlockPool 참조
    public float blockHeight = 8f; // 블록 간의 기본 높이 간격
    public float minBlockHeight = 25f; // 블록 간 최소 높이 간격
    public int visibleBlockCount; // 화면에 보이는 최대 블록 개수

    private float lastBlockY = 0f; // 마지막 블록의 Y 위치
    private Queue<GameObject> activeBlocks = new Queue<GameObject>(); // 활성 블록 큐

    private void Start()
    {
        if (blockPool == null)
        {
            blockPool = FindObjectOfType<BlockPool>();
            if (blockPool == null)
            {
                Debug.LogError("BlockPool not found in the scene!");
                return;
            }
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player object not found! Make sure it has the tag 'Player'.");
                return;
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

        // 플레이어 위치 기준으로 블록 생성
        if (player.position.y > lastBlockY - (visibleBlockCount * blockHeight / 1.5f))
        {
            if (activeBlocks.Count >= visibleBlockCount)
            {
                RemoveBlock(); // 초과된 블록 제거
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
        if (block == null)
        {
            Debug.LogError("Failed to create block.");
            return;
        }

        // X, Z 랜덤 범위 설정
        float randomX = Random.Range(-50f, 50f); // 넓어진 X 범위
        float randomZ = Random.Range(-50f, 50f); // Z축 범위 추가

        // Y 위치: 최소 높이 조건 적용
        float newBlockY = lastBlockY + Random.Range(minBlockHeight, blockHeight); // 최소 높이 간격 보장

        // 블록 위치 설정 및 활성화
        block.transform.position = new Vector3(randomX, newBlockY, randomZ);
        activeBlocks.Enqueue(block);

        lastBlockY = newBlockY; // 마지막 블록 위치 갱신
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
