using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChiselController : MonoBehaviour
{
    public GameObject toolSlot;
    private Rigidbody rb;

    // The hover display for handle
    public GameObject handleHover;

    public GameObject handle;

    // Grab information
    public string grabbingHand = "none";
    public GameObject handModelRight;
    public GameObject handModelLeft;
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

    public void HoverChisel()
    {
        HoverModel(true);
    }

    public void UnHoverChisel()
    {
        HoverModel(false);
    }

    public void HoldChisel()
    {
        handle.GetComponent<SphereCollider>().enabled = true;
        HoverModel(false);
    }

    public void UnHoldChisel()
    {
        handle.GetComponent<SphereCollider>().enabled = false;
    }


    public void OnSelect(DistanceGrabInteractable interactor)
    {

        //Debug.Log(interactor.Interactors.Count);
        //Debug.Log(interactor.Interactors.ToList()[0].gameObject.name);
        //Debug.Log(interactor.Interactors.ToList()[0].gameObject.transform.parent.parent.parent.name);

        if (interactor.Interactors.Count > 0)
        {
            //Debug.Log(interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name);
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Right"))
            {
                grabbingHand = "right";
                handModelRight.SetActive(false);
            }
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Left"))
            {
                grabbingHand = "left";
                handModelLeft.SetActive(false);
            }
        }
    }
    public void OnUnSelect()
    {
        grabbingHand = "none";
        handModelRight.SetActive(true);
        handModelLeft.SetActive(true);
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

