using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChunkController : MonoBehaviour
{
    [SerializeField] public int chunkHealth = 0;
    [SerializeField] private Vector2Int chunkPos;

    [SerializeField] private GameObject chunkObject;
    [SerializeField] public Stack<GameObject> chunks = new Stack<GameObject>();
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    // Resets the chunks health to a number
    public void SetChunkHealth(int newHealth)
    {
        chunkHealth = newHealth;
        ResetChunk();
    }
    public void SetChunkPos(Vector2Int newPos)
    {
        chunkPos = newPos;
    }

    public void ResetChunk()
    {
        // Clears all children
        chunks.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        // Spawns new chunks for the health
        if (chunkHealth <= 2) // Generating just 1 layer
        {
            GenerateChunk(chunkHealth, 1, 1);
        }
        else // Generating 1 layer and more
        {
            GenerateChunk(2, 1, 1);
            GenerateChunk(chunkHealth - 2, 2, 3);
        }

        chunks.Peek().GetComponent<BoxCollider>().enabled = true;
    }

    public void GenerateChunk(int newHealth, int newChunkType, float zPos)
    {
        GameObject newChunk = Instantiate(chunkObject);
        newChunk.GetComponent<MineCube>().SetHealth(newHealth);
        newChunk.GetComponent<MineCube>().SetChunkType(newChunkType);
        newChunk.GetComponent<MineCube>().SetChunkPos(chunkPos);
        newChunk.GetComponent<MineCube>().UpdateChunk();
        newChunk.GetComponent<BoxCollider>().enabled = false;
        newChunk.transform.name = "ChunkPiece";
        newChunk.transform.parent = transform;
        newChunk.transform.localPosition = Vector3.zero + new Vector3(0, 0, zPos * 0.125f);
        newChunk.transform.localRotation = Quaternion.identity;

        chunks.Push(newChunk);
    }

    public void DamageChunk(int damage)
    {
        if (chunks.Count > 0)
        {
            GameObject chunk = chunks.Peek() as GameObject;
            chunk.GetComponent<MineCube>().DamageChunk(damage);
            chunk.GetComponent<BoxCollider>().enabled = true;
        }
        
    }
}
