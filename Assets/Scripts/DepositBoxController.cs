using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBoxController : MonoBehaviour
{
    [SerializeField] private GameObject sellStorage;

    private void OnTriggerEnter(Collider other)
    {
        // Detects if loot entered container
        if (other.transform.parent.GetComponent<LootData>() != null)
        {
            other.transform.parent.parent = sellStorage.transform;
        }
    }
}
