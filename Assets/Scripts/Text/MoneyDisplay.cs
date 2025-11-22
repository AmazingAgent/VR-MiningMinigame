using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObj;
    [SerializeField] private PlayerData playerData;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textObj.text = "$" + playerData.GetMoney().ToString();
    }
}
