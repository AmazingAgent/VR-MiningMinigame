using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeHitEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private AudioSource audioSource;
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

        
        
    }

    public void DoParticle(bool isHammer)
    {
        if (timer <= 0f)
        {
            if (isHammer) { audioSource.pitch = Random.Range(0.5f, 0.7f); }
            else { audioSource.pitch = Random.Range(0.9f, 1.1f); }

            audioSource.Play();

            ps.Play();
            timer = cooldown;
        }
    }
}
