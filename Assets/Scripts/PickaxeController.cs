using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    public GameObject toolSlot;
    private Rigidbody rb;

    public Material holdMat;
    public Material releaseMat;
    public GameObject handle;
    public GameObject mineGrid;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handle.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = toolSlot.transform.position;
        //transform.localRotation = toolSlot.transform.localRotation;


        if (Vector3.Distance(transform.position, toolSlot.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, toolSlot.transform.position, 0.15f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, toolSlot.transform.localRotation, 0.1f);
        }
        else
        {
            transform.position = toolSlot.transform.position;
            transform.localRotation = toolSlot.transform.localRotation;
        }
    }

    public void HoverPickaxe()
    {
        handle.GetComponent<MeshRenderer>().material = holdMat;
    }

    public void UnHoverPickaxe()
    {
        handle.GetComponent<MeshRenderer>().material = releaseMat;
    }

    public void HoldPickaxe()
    {
        handle.GetComponent<SphereCollider>().enabled = true;
    }

    public void ReleasePickaxe()
    {
        handle.GetComponent<SphereCollider>().enabled = false;
        mineGrid.GetComponent<PickaxeDetector>().RemoveGameObject(handle);
    }
}

