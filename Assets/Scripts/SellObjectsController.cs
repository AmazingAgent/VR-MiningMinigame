using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellObjectsController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        foreach (Transform obj in transform)
        {
            LootData lootData = obj.gameObject.GetComponent<LootData>();
            if (lootData != null)
            {
                playerData.AddMoney(lootData.GetPrice());
            }
            Destroy(obj.gameObject);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}
