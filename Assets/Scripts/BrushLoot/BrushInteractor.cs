using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrushInteractor : MonoBehaviour
{
    [SerializeField] private LootData lootData;
    [SerializeField] private int priceIncrease = 5;

    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private bool isBrushed = false;

    [SerializeField] private Mesh brushedVersion;
    [SerializeField] private Material[] brushedMaterials;
    private LootHover lootHover;

    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        lootHover = GetComponent<LootHover>();
    }

    public void UnlockBrushInteraction()
    {
        isUnlocked = true;
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision");
        if (isUnlocked && !isBrushed && collision.gameObject.CompareTag("Brush"))
        {
            Debug.Log("brushed");
            GetComponent<MeshFilter>().mesh = brushedVersion;
            GetComponent<MeshRenderer>().materials = brushedMaterials;
            GetComponent<MeshCollider>().sharedMesh = brushedVersion;
            lootHover.SetMaterials(brushedMaterials);
            isBrushed = true;

            collision.gameObject.GetComponent<BrushHitEffects>().DoParticle();

            lootData.IncreasePrice(priceIncrease);

            DoParticle();
        }
    }
    
    /*
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        if (isUnlocked && !isBrushed && other.gameObject.CompareTag("Brush"))
        {
            Debug.Log("brushed");
            GetComponent<MeshFilter>().mesh = brushedVersion;
            GetComponent<MeshRenderer>().materials = brushedMaterials;
            GetComponent<MeshCollider>().sharedMesh = brushedVersion;
            isBrushed = true;

            other.gameObject.GetComponent<BrushHitEffects>().DoParticle();

            lootData.IncreasePrice(priceIncrease);

            DoParticle();
        }
    }
    */

    public void DoParticle()
    {
        ps.Play();
    }

}
