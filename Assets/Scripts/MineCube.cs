using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCube : MonoBehaviour
{
    private PickaxeDetector detector;
    private MineGridController gridController;
    [SerializeField] Mesh chunkMesh;
    [SerializeField] Mesh chunkCrackedMesh;

    // Chunk data
    [SerializeField] private int health = 2;
    [SerializeField] private Vector2Int chunkPos;

    private void Start()
    {
        detector = transform.parent.GetComponentInParent<PickaxeDetector>();
        gridController = transform.parent.GetComponentInParent<MineGridController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe") && detector.colliders.Contains(other.gameObject))
        {
            int toolState = other.gameObject.GetComponent<TogglePickaxe>().toolState;

            if (toolState == 0) // Pickaxe hit
            {
                DamageChunk(2);
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(1, 0));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(-1, 0));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(0, 1));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(0, -1));
            }
            if (toolState == 1) // Hammer hit
            {
                DamageChunk(2);
                gridController.DamageChunkAtPos(2, chunkPos + new Vector2Int(1, 0));
                gridController.DamageChunkAtPos(2, chunkPos + new Vector2Int(-1, 0));
                gridController.DamageChunkAtPos(2, chunkPos + new Vector2Int(0, 1));
                gridController.DamageChunkAtPos(2, chunkPos + new Vector2Int(0, -1));

                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(1, 1));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(-1, 1));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(-1, 1));
                gridController.DamageChunkAtPos(1, chunkPos + new Vector2Int(-1, -1));
            }

            detector.RemoveGameObject(other.gameObject);
        }
    }

    public void SetChunkPos(Vector2Int newPos)
    {
        chunkPos = newPos;
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth;
        UpdateChunk();
    }

    // Damages the chunk
    public void DamageChunk(int damage)
    {
        

        if (health - damage <= 0)
        {
            // Pops the item from the stack
            if (transform.parent.GetComponent<GridChunkController>().chunks.Count > 0) transform.parent.GetComponent<GridChunkController>().chunks.Pop();

            transform.parent.GetComponent<GridChunkController>().DamageChunk(damage - health);
            transform.parent.GetComponent<GridChunkController>().chunkHealth -= health;
            health = 0;
        }
        else
        {
            transform.parent.GetComponent<GridChunkController>().chunkHealth -= damage;
            health -= damage;
        }

        UpdateChunk();
    }

    // Updates the model or destroys the chunk
    private void UpdateChunk()
    {
        switch (health)
        {
            case 0:
                Destroy(this.gameObject);
                break;
            case 1:
                gameObject.GetComponent<MeshFilter>().mesh = chunkCrackedMesh;
                break;
            case 2:
                gameObject.GetComponent<MeshFilter>().mesh = chunkMesh;
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }
}
