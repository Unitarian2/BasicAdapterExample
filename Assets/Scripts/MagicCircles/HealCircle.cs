using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour, ICircle
{
    private int healAmount;
    public float Amount { get; set; }
    public StatType Type { get; set; }

    public float GetCalculatedAmount()
    {
        return Amount;
    }

    private void Start()
    {
        Amount = 5;
        Type = StatType.Health;
    }
}
