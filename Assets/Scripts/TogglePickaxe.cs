using System.Collections;
using System.Collections.Generic;
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
    private void Update()
    {
        // Pressed the right trigger
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            EquipHammer();
        }
        // Released the right trigger
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
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
