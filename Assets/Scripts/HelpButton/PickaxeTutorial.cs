using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeTutorial : MonoBehaviour
{

    [SerializeField] private GameObject tutorial;

    void Start()
    {
        tutorial.SetActive(false);
    }

    public void ActivateTutorial()
    {
        tutorial.SetActive(true);
    }

    public void DeactivateTutorial() {
        tutorial.SetActive(false);
    }

}
