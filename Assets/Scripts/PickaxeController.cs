using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    public GameObject toolSlot;
    private Rigidbody rb;

    // The hover display for handle
    public GameObject handleHover;

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
            transform.rotation = Quaternion.Lerp(transform.localRotation, toolSlot.transform.rotation, 0.1f);
        }
        else
        {
            transform.position = toolSlot.transform.position;
            transform.rotation = toolSlot.transform.rotation;
        }
    }

    public void HoverPickaxe()
    {
        HoverModel(true);
    }

    public void UnHoverPickaxe()
    {
        HoverModel(false);
    }

    public void HoldPickaxe()
    {
        handle.GetComponent<SphereCollider>().enabled = true;
        HoverModel(false);
    }

    public void ReleasePickaxe()
    {
        handle.GetComponent<SphereCollider>().enabled = false;
        mineGrid.GetComponent<PickaxeDetector>().RemoveGameObject(handle);
    }


    private void HoverModel(bool isHover)
    {
        // Toggle hover model
        handleHover.GetComponent<MeshRenderer>().enabled = isHover;
        foreach (MeshRenderer renderer in handleHover.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = isHover;
        }

        // Toggle normal model
        handle.GetComponent<MeshRenderer>().enabled = !isHover;
        foreach (MeshRenderer renderer in handle.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = !isHover;
        }
    }
}

