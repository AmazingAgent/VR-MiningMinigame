using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationItemDetector : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<LootData>() != null)
        {
            tutorial.AttemptPlaceItemDown();
        }
    }
}
