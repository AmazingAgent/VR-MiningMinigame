using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private List<WallVisibility> wallObjects = new List<WallVisibility>();

    void Start()
    {
        foreach (Transform obj in transform)
        {
            wallObjects.Add(obj.gameObject.GetComponent<WallVisibility>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWall()
    {
        foreach (WallVisibility obj in wallObjects)
        {
            obj.CloseWall();
        }
    }

    public void OpenWall()
    {
        foreach (WallVisibility obj in wallObjects)
        {
            obj.OpenWall();
        }
    }
}
