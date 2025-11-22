using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isTopWall;
    public float basePosY;
    private float targetPosY;
    [SerializeField] public bool isAtTarget = true;
    [SerializeField] private MineGridController controller;
    [SerializeField] private float loadDelay;

    void Start()
    {
        basePosY = transform.localPosition.y;
        CloseWall();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();

        if (transform.localPosition.y != targetPosY)
        {
            //isAtTarget = false;
            MoveWall();
        }
        else
        {

        }

        if (!isAtTarget && transform.localPosition.y == basePosY)
        {
            isAtTarget = true;
            if (controller != null)
            {
                StartCoroutine(DelayWallLoad());
            }
        }
    }

    private IEnumerator DelayWallLoad()
    {
        Debug.Log("start load");
        yield return new WaitForSeconds(1);
        controller.InitializeGrid();
        Debug.Log("end load");
    }
    public void CloseWall()
    {
        isAtTarget = false;
        targetPosY = basePosY;
        //Debug.Log("Closing: " + gameObject.name + " " + basePosY + " " + targetPosY);
    }

    public void OpenWall()
    {
        isAtTarget = false;
        if (isTopWall)
        {
            targetPosY = basePosY - 3.1f;
        }
        else
        {
            targetPosY = basePosY + 3.1f;
        }
        //Debug.Log("Opening: " + gameObject.name + " " + basePosY + " " + targetPosY);
    }
    private void MoveWall()
    {
        if (transform.localPosition.y < targetPosY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + moveSpeed * Time.deltaTime, transform.localPosition.z);
            if (transform.localPosition.y > targetPosY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, targetPosY, transform.localPosition.z);
                //isAtTarget = true;
            }
        }
        if (transform.localPosition.y > targetPosY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - moveSpeed * Time.deltaTime, transform.localPosition.z);
            if (transform.localPosition.y < targetPosY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, targetPosY, transform.localPosition.z);
                //isAtTarget = true;
            }
        }
    }
    public void CheckVisibility()
    {
        if (Mathf.Abs(transform.localPosition.y) > 3)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
