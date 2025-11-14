using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TogglePickaxe : MonoBehaviour
{
    //private bool isHammer = false;
    private float rotSpeed = 15f;
    private Quaternion targetRot;

    public GameObject pickaxeHead;

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


        if (Quaternion.Angle(pickaxeHead.transform.localRotation, targetRot) > 10f)
        {
            pickaxeHead.transform.localRotation = Quaternion.Lerp(pickaxeHead.transform.localRotation, targetRot, Time.deltaTime * rotSpeed);
        }
        else
        {
            pickaxeHead.transform.localRotation = targetRot;
        }
    }

    public void EquipHammer()
    {
        toolState = 1;
        targetRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, 180);
        
    }

    public void EquipPickaxe()
    {
        toolState = 0;
        targetRot = Quaternion.Euler(pickaxeHead.transform.localRotation.x, pickaxeHead.transform.localRotation.y, 0);
    }

}
