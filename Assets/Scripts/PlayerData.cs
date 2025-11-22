using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int money = 0;
    [SerializeField] private int lootMined = 0;
    
    public void AddMoney(int newMoney)
    {
        money += newMoney;
    }
    public void SubtractMoney(int newMoney)
    {
        money -= newMoney;
    }
    public int GetMoney()
    {
        return money;
    }

    public void MinedLoot()
    {
        lootMined += 1;
    }
}
