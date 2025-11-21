using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiselInteractor : MonoBehaviour
{
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private bool isChiseled = false;

    [SerializeField] private Mesh chiseledVersion;

    
    public void UnlockChiselInteraction()
    {
        isUnlocked = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isUnlocked && !isChiseled && collision.gameObject.CompareTag("Chisel"))
        {
            GetComponent<MeshFilter>().mesh = chiseledVersion;
            GetComponent<MeshCollider>().sharedMesh = chiseledVersion;
            isChiseled = true;

            collision.gameObject.GetComponent<ChiselHitEffects>().DoParticle();
        }
    }
}
