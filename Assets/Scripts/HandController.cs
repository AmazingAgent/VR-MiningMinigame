using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        rightHand.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
        rightHand.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

        leftHand.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        leftHand.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
    }

    public void HideHand(DistanceGrabInteractable interactor)
    {
        if (interactor.Interactors.Count > 0)
        {
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Right"))
            {
                rightHand.SetActive(false);
            }
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Left"))
            {
                leftHand.SetActive(false);
            }
        }
    }

    public void ShowHand(DistanceGrabInteractable interactor)
    {
        if (interactor.Interactors.Count > 0)
        {
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Right"))
            {
                rightHand.SetActive(true);
            }
            if (interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name.Contains("Left"))
            {
                leftHand.SetActive(true);
            }
        }
    }
}
