using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushHitEffects : MonoBehaviour
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

    public void DoParticle()
    {
        if (timer <= 0f)
        {
            audioSource.pitch = Random.Range(1.2f, 1.4f);
            audioSource.Play();

            ps.Play();
            timer = cooldown;
        }
    }
}
