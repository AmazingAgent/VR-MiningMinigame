using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGridController : MonoBehaviour
{
    // Loot grid
    [SerializeField] private GameObject lootGrid;

    // Grid information
    [SerializeField] private Vector2Int gridDimensions;
    private int[,] gridData; // Stores occupation data for chunks (0 for open | 1 for occupied)
    private Vector2 gridOffset;


    // Loot objects
    [SerializeField] GameObject[] lootObjects;
    private List<GameObject> loot; // Stores all loot objects on the grid
    void Start()
    {
        GenerateGrid();
        PopulateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateGrid()
    {
        gridOffset = new Vector2((gridDimensions.x / 2f), (gridDimensions.y / 2f));
        gridData = new int[gridDimensions.x, gridDimensions.y];

        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                gridData[ix, iy] = 0;
            }
        }
    }

    private void PopulateGrid()
    {
        // Grab random object to try and put in grid
        GameObject randomObj = lootObjects[Random.Range(0, lootObjects.Length)];

        if (IsEmpty(new Vector2Int(1, 1))) { 
            PlaceLoot(randomObj, new Vector2Int(1, 1), 0);
        }
        if (IsEmpty(new Vector2Int(1, 1)))
        {
            PlaceLoot(randomObj, new Vector2Int(1, 1), 0);
        }


    }

    private void PlaceLoot(GameObject lootObj, Vector2Int lootPos, int lootRot)
    {

        GameObject newLoot = Instantiate(lootObj);
        newLoot.transform.parent = lootGrid.transform;
        newLoot.transform.rotation = lootGrid.transform.rotation;
        newLoot.transform.localPosition = new Vector3(lootPos.x - gridOffset.x, lootPos.y - gridOffset.y, 0);

        loot.Add(newLoot);

        gridData[lootPos.x, lootPos.y] = 1;
    }

    // Checks if the chunk at a position is occupied
    private bool IsEmpty(Vector2Int slotPos)
    {
        if (gridData[slotPos.x, slotPos.y] == 0) { return true; }
        return false;
    }
}
