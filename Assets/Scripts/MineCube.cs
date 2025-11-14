using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCube : MonoBehaviour
{
    private PickaxeDetector detector;
    [SerializeField] Mesh chunkMesh;
    [SerializeField] Mesh chunkCrackedMesh;

    // Chunk data
    [SerializeField] private int health = 2;
    [SerializeField] private Vector2Int chunkPos;

    private void Start()
    {
        detector = transform.parent.GetComponentInParent<PickaxeDetector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe") && detector.colliders.Contains(other.gameObject))
        {
            int toolState = other.gameObject.GetComponent<TogglePickaxe>().toolState;

            if (toolState == 0) DamageChunk(1);

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
        health -= damage;

        if (health < 0)
        {
            transform.parent.GetComponent<GridChunkController>().DamageChunk(damage);
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
