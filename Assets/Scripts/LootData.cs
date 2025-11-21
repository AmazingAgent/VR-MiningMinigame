using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootData : MonoBehaviour
{
    //public bool unlocked = false;
    [SerializeField] GameObject grabHandle;
    [SerializeField] GameObject background;
    [SerializeField] Vector3 backgroundPos;
    private GameObject objectStorage;
    public GameObject lootPanel;

    [SerializeField] private List<Vector2Int> occupiedSlots = new List<Vector2Int>();

    [SerializeField] private Vector2Int[] chunkSlots;
    private MineGridController gridData;



    private void Update()
    {
        TryToFreeObject();
    }




    // Get the positions of occupied slots
    // rotation is in multiples of 90 degrees (rot = 0 = 0deg | rot = 1 = 90deg | rot = 2 = 180deg | rot = 3 = 270 deg)
    public List<Vector2Int> GetOccupiedSlots(int rotation)
    {
        List<Vector2Int> newOccupiedSlots = new List<Vector2Int>();
        newOccupiedSlots.Clear();

        switch (rotation)
        {
            case 0: // 0 degrees
                return occupiedSlots;

            case 1: // 90 degrees
                foreach (Vector2Int slot in occupiedSlots)
                {
                    newOccupiedSlots.Add(new Vector2Int(slot.y, slot.x * -1));
                }

                return newOccupiedSlots;

            case 2: // 180 degrees
                foreach (Vector2Int slot in occupiedSlots)
                {
                    newOccupiedSlots.Add(new Vector2Int(slot.x * -1, slot.y * -1));
                }
                return newOccupiedSlots;

            case 3: // 270 degrees
                foreach (Vector2Int slot in occupiedSlots)
                {
                    newOccupiedSlots.Add(new Vector2Int(slot.y * -1, slot.x));
                }
                return newOccupiedSlots;

            default:
                return occupiedSlots;
        }
    }

    public void SetChunkSlots(Vector2Int baseChunkPos, int rotation, MineGridController newGridData)
    {
        List<Vector2Int> gridPositions = new List<Vector2Int>();
        chunkSlots = new Vector2Int[occupiedSlots.Count];

        //Debug.Log("setting chunks");
        gridPositions = GetOccupiedSlots(rotation);
        int i = 0;
        foreach (Vector2Int gridPos in gridPositions)
        {
            chunkSlots[i] = gridPos + baseChunkPos;
            //Debug.Log("addslot" + (chunkSlots[i] + baseChunkPos));
            i++;
        }

        gridData = newGridData;


        backgroundPos = background.transform.position - lootPanel.transform.position;
    }

    public bool CheckChunkSlots()
    {
        if (chunkSlots.Length > 0)
        {
            bool isFreed = true;
            foreach (Vector2Int slot in chunkSlots)
            {
                if (gridData.GetGridData(slot) != 0)
                {
                    isFreed = false;
                }
            }
            return isFreed;
        }
        return false;
    }

    public void TryToFreeObject()
    {
        if (CheckChunkSlots())
        {
            grabHandle.SetActive(true);
        }
    }

    public void GrabbedObject()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.parent = objectStorage.transform;
        if (background != null)
        {
            background.transform.parent = lootPanel.transform;
            background.transform.position = backgroundPos + lootPanel.transform.position;
        }
    }


    public void SetObjectStorage(GameObject newStorageObj)
    {
        objectStorage = newStorageObj;
    }
}
