using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGridController : MonoBehaviour
{
    // Grid information
    [SerializeField] private Vector2Int gridDimensions;
    [SerializeField] private float gridHealth;
    [SerializeField] private float gridCurrentHealth;
    private int[,] gridData;
    private GameObject[,] chunkData;
    private Vector2 gridOffset;

    // Grid gameobjects
    [SerializeField] private GameObject gridChunk;
    [SerializeField] private GameObject gridParent;

    // Loot grid
    [SerializeField] private LootGridController lootGridController;
    [SerializeField] private GridHealthbar gridHealthbar;

    // Wall Gameobject
    [SerializeField] private WallController wallController;
    [SerializeField] private WallVisibility topWall;
    [SerializeField] private bool loading = true;
    [SerializeField] private ResetButton resetButton;

    // Pickaxe Detector
    [SerializeField] private PickaxeDetector pickaxeDetector;

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

    public void ResetGrid()
    {
        if (!loading)
        {
            loading = true;
            pickaxeDetector.Disable();
            wallController.CloseWall();
        }
    }

    // Initialize the grid
    public void InitializeGrid()
    {
        //wallController.CloseWall();

        lootGridController.InitializeGrid();
        ClearGrid();
        GenerateGrid();
        SpawnGrid();
        gridHealthbar.UpdateHealth(gridCurrentHealth);

        wallController.OpenWall();
        loading = false;
        pickaxeDetector.Enable();
        resetButton.ActivateButton();
    }

    private void ClearGrid()
    {
        foreach (Transform child in gridParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Generates the data for each grid space
    private void GenerateGrid()
    {
        // Reset the grid health
        gridCurrentHealth = gridHealth;
        gridHealthbar.SetMaxHealth(gridHealth);

        // Setup size of grid
        gridOffset = new Vector2((gridDimensions.x/2f) - 0.5f, (gridDimensions.y/2f) - 0.5f);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(gridDimensions.x * 0.1f, gridDimensions.y * 0.1f, 0.15f);

        gridData = new int[gridDimensions.x, gridDimensions.y];
        chunkData = new GameObject[gridDimensions.x, gridDimensions.y];
        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                gridData[ix, iy] = Random.Range(2,6);
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

                chunkData[ix,iy] = newChunk;
            }
        }
    }


    public void DamageChunkAtPos(int damage, Vector2Int damagePos)
    {
        // Check if inside dimensions
        if (damagePos.x >= 0 && damagePos.x < gridDimensions.x && damagePos.y >= 0 && damagePos.y < gridDimensions.y)
        {
            chunkData[damagePos.x, damagePos.y].GetComponent<GridChunkController>().DamageChunk(damage);
        }
        //UpdateChunkData();
    }

    private void UpdateChunkData()
    {
        foreach (var chunk in chunkData)
        {
            Vector2Int chunkPos = chunk.GetComponent<GridChunkController>().chunkPos;
            gridData[chunkPos.x, chunkPos.y] = chunk.GetComponent<GridChunkController>().chunkHealth;
        }
    }

    public void DamageGrid(int damage)
    {
        gridCurrentHealth -= damage;
        if (gridCurrentHealth <= 0) // Grid ran out of health
        {
            gridCurrentHealth = 0;
            gridHealthbar.UpdateHealth(gridCurrentHealth);

            ResetGrid();
        }
        gridHealthbar.UpdateHealth(gridCurrentHealth);
    }


    public int GetGridData(Vector2Int pos)
    {
        UpdateChunkData();
        //Debug.Log(gridData.Length);
        return gridData[pos.x, pos.y];
    }
}
