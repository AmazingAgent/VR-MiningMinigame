using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TogglePickaxe : MonoBehaviour
{
    //private bool isHammer = false;
    private float rotSpeed = 15f;
    private Quaternion targetHeadRot;
    private Quaternion targetGearRot;

    public GameObject pickaxeHead;
    public GameObject pickaxeGear;

    // The current tool state
    // 0 = Pickaxe | 1 = Hammer
    public int toolState = 0;


    // Grab information
    public string grabbingHand = "none";
    public GameObject handModelRight;
    public GameObject handModelLeft;
    private void Update()
    {

        // Pressed the right trigger
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && grabbingHand == "right")
        {
            EquipHammer();
        }

        // Released the right trigger
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && grabbingHand == "right")
        {
            EquipPickaxe();
        }

        // Pressed the left trigger
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) && grabbingHand == "left")
        {
            EquipHammer();
        }

        // Released the left trigger
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) && grabbingHand == "left")
        {
            EquipPickaxe();
        }


        if (Quaternion.Angle(pickaxeHead.transform.localRotation, targetHeadRot) > 10f)
        {
            pickaxeHead.transform.localRotation = Quaternion.Lerp(pickaxeHead.transform.localRotation, targetHeadRot, Time.deltaTime * rotSpeed);
            pickaxeGear.transform.localRotation = Quaternion.Lerp(pickaxeGear.transform.localRotation, targetGearRot, Time.deltaTime * rotSpeed);
        }
        else
        {
            pickaxeHead.transform.localRotation = targetHeadRot;
            pickaxeGear.transform.localRotation = targetGearRot;
        }
    }

    public void OnSelect(DistanceGrabInteractable interactor)
    {

        //Debug.Log(interactor.Interactors.Count);
        //Debug.Log(interactor.Interactors.ToList()[0].gameObject.name);
        //Debug.Log(interactor.Interactors.ToList()[0].gameObject.transform.parent.parent.parent.name);

        if (interactor.Interactors.Count > 0)
        {
            Debug.Log(interactor.Interactors.ToList().Last().gameObject.transform.parent.parent.parent.name);
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

        EquipPickaxe();
    }
    public void EquipHammer()
    {
        toolState = 1;
        targetHeadRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, 180);
        targetGearRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, -180);

    }

    public void EquipPickaxe()
    {
        toolState = 0;
        targetHeadRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, 0);
        targetGearRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, 0);
    }

}
