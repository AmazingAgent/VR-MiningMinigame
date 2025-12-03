using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPanelUpdateCheck : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    private void OnTransformChildrenChanged()
    {
        tutorial.AttemptGrabItem();
    }
}
