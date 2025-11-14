using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGridController : MonoBehaviour
{
    // Grid information
    [SerializeField] private Vector2Int gridDimensions;
    private int[,] gridData;
    private Vector2 gridOffset;

    // Grid gameobjects
    [SerializeField] private GameObject gridChunk;
    [SerializeField] private GameObject gridParent;

    void Start()
    {
        // Initialize the grid
        GenerateGrid();
        SpawnGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generates the data for each grid space
    private void GenerateGrid()
    {
        // Setup size of grid
        gridOffset = new Vector2((gridDimensions.x/2f) - 0.5f, (gridDimensions.y/2f) - 0.5f);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(gridDimensions.x * 0.1f, gridDimensions.y * 0.1f, 0.15f);

        gridData = new int[gridDimensions.x, gridDimensions.y];
        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                gridData[ix, iy] = Random.Range(1,5);
                //gridData[ix, iy] = 3;
            }
        }
    }

    // Spawns tiles based on the grid data
    private void SpawnGrid()
    {
        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                GameObject newChunk = Instantiate(gridChunk);
                newChunk.transform.name = "GridChunk";
                newChunk.transform.parent = gridParent.transform;
                newChunk.transform.localPosition = new Vector3(ix - gridOffset.x, iy - gridOffset.y, 0) * 0.1f;
                newChunk.transform.localRotation = Quaternion.identity;
                newChunk.GetComponent<GridChunkController>().SetChunkPos(new Vector2Int(ix, iy));
                newChunk.GetComponent<GridChunkController>().SetChunkHealth(gridData[ix, iy]);
                
            }
        }
    }

}
