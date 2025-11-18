using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeHitEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float cooldown = 0.1f;
    private float timer = 0f;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (timer <= 0f && !other.CompareTag("NoPickaxeHit"))
        {
            ps.Play();
            timer = cooldown;
        }
        
    }
}
