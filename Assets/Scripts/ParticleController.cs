using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Particle objects
    [SerializeField] GameObject[] particleObjects;
    private List<GameObject> aliveParticles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckParticles()
    {
        foreach (GameObject partObj in particleObjects)
        {
            if (partObj == null)
            {
                aliveParticles.Remove(partObj);
            }
        }
    }
    public void SpawnParticle(int partIndex, Vector3 partPos, Vector3 partRot, float partLifetime)
    {
        GameObject newParticle = Instantiate(particleObjects[partIndex]);
        newParticle.GetComponent<ParticleObject>().SetData(partPos, partRot, partLifetime);
        newParticle.transform.parent = transform;

        aliveParticles.Add(newParticle);
    }
}
