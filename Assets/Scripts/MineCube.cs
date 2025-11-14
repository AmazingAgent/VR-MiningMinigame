using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            Debug.Log("Collider");

            Destroy(this.gameObject);
        }
    }
}
