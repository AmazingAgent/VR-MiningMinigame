using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TogglePickaxe : MonoBehaviour
{
    //private bool isHammer = false;
    private float rotSpeed = 15f;
    private Quaternion targetRot;

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


        if (Quaternion.Angle(transform.localRotation, targetRot) > 10f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * rotSpeed);
        }
        else
        {
            transform.localRotation = targetRot;
        }
    }

    public void EquipHammer()
    {
        targetRot = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 180);
        
    }

    public void EquipPickaxe()
    {
        targetRot = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0);
    }

}
