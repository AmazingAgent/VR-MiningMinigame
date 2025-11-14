using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCube : MonoBehaviour
{
    private PickaxeDetector detector;

    private void Start()
    {
        detector = transform.parent.GetComponentInParent<PickaxeDetector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe") && detector.colliders.Contains(other.gameObject))
        {
            int toolState = other.gameObject.GetComponent<TogglePickaxe>().toolState;

            if (toolState == 0) Destroy(this.gameObject);
            
            detector.RemoveGameObject(other.gameObject);
        }
    }
}
