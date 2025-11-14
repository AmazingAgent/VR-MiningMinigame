using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickaxeDetector : MonoBehaviour
{

    public List<GameObject> colliders = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            colliders.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            colliders.Remove(other.gameObject);
        }
    }

    public void RemoveGameObject(GameObject obj)
    {
        colliders.Remove(obj);
    }
}
