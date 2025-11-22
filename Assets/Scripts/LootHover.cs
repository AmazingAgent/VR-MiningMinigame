using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHover : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private Material[] newMats;
    void Start()
    {
        // Get old materials
        originalMaterials = GetComponent<MeshRenderer>().materials;

        // Get new materials
        int i = 0;
        newMats = new Material[originalMaterials.Length];
        foreach (Material mat in originalMaterials)
        {
            newMats[i] = hoverMaterial;
            i++;
        }
    }

    public void OnHover()
    {
        GetComponent<MeshRenderer>().materials = newMats;
    }

    public void OnUnHover()
    {
        GetComponent<MeshRenderer>().materials = originalMaterials;
    }

    public void OnSelect(DistanceGrabInteractable interactor)
    {
        GameObject.Find("CustomHands").GetComponent<HandController>().HideHand(interactor);
    }

    public void OnUnSelect(DistanceGrabInteractable interactor)
    {
        GameObject.Find("CustomHands").GetComponent<HandController>().ShowHand(interactor);
    }

    public void SetMaterials(Material[] newMaterials)
    {
        originalMaterials = newMaterials;
    }
}
