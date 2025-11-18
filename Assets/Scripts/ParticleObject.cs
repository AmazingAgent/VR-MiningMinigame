using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private float lifetime;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            if (lifetime <= 0) Destroy(gameObject);
        }
    }

    public void SetData(Vector3 pos, Vector3 rot, float time)
    {
        transform.position = pos;
        transform.eulerAngles = rot;
        lifetime = time;
        ps.Play();
    }
}
