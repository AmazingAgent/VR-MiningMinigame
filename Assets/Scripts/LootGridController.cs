using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGridController : MonoBehaviour
{
    // Loot grid
    [SerializeField] private GameObject lootGrid;
    [SerializeField] private MineGridController mineGrid;
    [SerializeField] private GameObject objectStorage;

    // Grid information
    [SerializeField] private Vector2Int gridDimensions;
    private int[,] gridData; // Stores occupation data for chunks (0 for open | 1 for occupied)
    private Vector2 gridOffset;


    // Loot objects
    [SerializeField] GameObject[] lootObjects;
    private List<GameObject> loot = new List<GameObject>(); // Stores all loot objects on the grid
    [SerializeField] private GameObject backgroundObject;
    void Start()
    {
        InitializeGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeGrid()
    {
        ClearGrid();
        GenerateGrid();
        PopulateGrid();
    }

    // Clears all loot data
    private void ClearGrid()
    {
        gridOffset = new Vector2((gridDimensions.x / 2f), (gridDimensions.y / 2f));
        gridData = new int[gridDimensions.x, gridDimensions.y];

        foreach (Transform child in lootGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                gridData[ix, iy] = 0;
            }
        }

        loot.Clear();
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
        //GameObject randomObj = lootObjects[0];
        GameObject randomObj;
        GameObject lootObjToSpawn;

        int objectCount = Random.Range(2, 5);
        int tries = 100;

        while (objectCount > 0 && tries > 0)
        {
            randomObj = lootObjects[Random.Range(0, lootObjects.Length)];
            lootObjToSpawn = Instantiate(randomObj, transform.position, transform.rotation);

            if (TryToSpawnLoot(lootObjToSpawn)) objectCount--;
            tries--;
        }

        // Fill in the empty spaces with blank backgrounds
        for (int ix = 0; ix < gridDimensions.x; ix++)
        {
            for (int iy = 0; iy < gridDimensions.y; iy++)
            {
                if (gridData[ix, iy] == 0) {
                    GameObject newBackground = Instantiate(backgroundObject, transform.position, transform.rotation);
                    newBackground.transform.parent = lootGrid.transform;
                    newBackground.transform.rotation = lootGrid.transform.rotation;
                    newBackground.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newBackground.transform.localPosition = new Vector3(ix - gridOffset.x, iy - gridOffset.y, 0);
                }
            }
        }
    }



    private bool TryToSpawnLoot(GameObject lootObj)
    {
        List<Vector3Int> possiblePositions = new List<Vector3Int>();
        List<Vector2Int> lootPositions = new List<Vector2Int>();

        // Checks if it is possible to spawn here
        for (int rotation = 0; rotation <= 3; rotation++)
        {
            lootPositions = lootObj.GetComponent<LootData>().GetOccupiedSlots(rotation);

            for (int ix = 0; ix < gridDimensions.x; ix++)
            {
                for (int iy = 0; iy < gridDimensions.y; iy++)
                {
                    if (IsSpaceAvailable(new Vector2Int(ix,iy), lootPositions))
                    {
                        possiblePositions.Add(new Vector3Int(ix,iy,rotation));
                    }
                }
            }
        }

        if (possiblePositions.Count > 0)
        {
            Vector3Int spawnedLootPos = possiblePositions[Random.Range(0, possiblePositions.Count)];
            PlaceLoot(lootObj, new Vector2Int(spawnedLootPos.x, spawnedLootPos.y), spawnedLootPos.z);
            return true;
        }
        Destroy(lootObj);
        return false;
    }



    private void PlaceLoot(GameObject newLoot, Vector2Int lootPos, int lootRot)
    {


        newLoot.transform.parent = lootGrid.transform;
        newLoot.transform.rotation = lootGrid.transform.rotation;
        newLoot.transform.localEulerAngles = new Vector3(0,0,-90 * lootRot);
        newLoot.transform.localPosition = new Vector3(lootPos.x - gridOffset.x, lootPos.y - gridOffset.y, 0);

        newLoot.GetComponent<LootData>().lootPanel = lootGrid;
        newLoot.GetComponent<LootData>().SetChunkSlots(lootPos, lootRot, mineGrid);
        newLoot.GetComponent<LootData>().SetObjectStorage(objectStorage);
        
        //loot.Add(newLoot);

        OccupySpaces(lootPos, newLoot.GetComponent<LootData>().GetOccupiedSlots(lootRot));
        
    }

    private void OccupySpaces(Vector2Int basePos, List<Vector2Int> slotPositions)
    {
        foreach (Vector2Int position in slotPositions)
        {
            gridData[basePos.x + position.x, basePos.y + position.y] = 1;
        }
    }

    
    private bool IsSpaceAvailable(Vector2Int basePos, List<Vector2Int> slotPositions)
    {
        bool available = true;
        foreach (Vector2Int slotPosition in slotPositions)
        {
            if (!IsSlotEmpty(basePos + slotPosition)) { available = false; break; }
        }
        return available;
    }

    // Checks if the chunk at a position is occupied
    private bool IsSlotEmpty(Vector2Int slotPos)
    {
        if (slotPos.x >= 0 && slotPos.y >= 0 && slotPos.x < gridDimensions.x && slotPos.y < gridDimensions.y)
        {
            if (gridData[slotPos.x, slotPos.y] == 0) { return true; }
        }
        return false;
    }
}
