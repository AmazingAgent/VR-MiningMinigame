using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHealthbar : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float maxTranslation;

    [SerializeField] private float maxHealth;
    [SerializeField] private float boardHealth;
    

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    public void UpdateHealth(float newHealth)
    {
        boardHealth = newHealth;
        UpdateModel();
    }

    private void UpdateModel()
    {
        transform.localPosition = new Vector3(maxTranslation * (1 - (boardHealth / maxHealth)), transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(maxScale * (boardHealth / maxHealth), transform.localScale.y, transform.localScale.z);
    }
}
