using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Subject, IObserver
{
    [SerializeField] Subject _playerInteractionSubject;
    [SerializeField] Subject _playerMovementSubject;

    public static event Action<float,float> OnHealthChanged;
    public static event Action<float,float> OnManaChanged;

    private float health;
    private float mana;

    public float CurrentHealth { get => health; set => health = value; }
    public float CurrentMana { get => mana; set => mana = value; }

    public const float maxHealth = 100;
    public const float minHealth = 0;

    public const float maxMana = 150;
    public const float minMana = 0;

    private void Start()
    {
        CurrentHealth = maxHealth;
        CurrentMana = maxMana;
    }

    public bool IsManaEnough()
    {
        if(CurrentMana == minMana)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnNotify(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Health:
                CurrentHealth = Mathf.Clamp((amount+ CurrentHealth), minHealth, maxHealth);
                InvokeHealthChanged(CurrentHealth,maxHealth);
                break;
            case StatType.Mana:
                CurrentMana = Mathf.Clamp((amount + CurrentMana), minMana, maxMana);
                InvokeManaChanged(CurrentMana,maxMana);
                break;
        }
    }

    private void OnEnable()
    {
        _playerInteractionSubject.AddObserver(this);
        _playerMovementSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerInteractionSubject.RemoveObserver(this);
        _playerMovementSubject.RemoveObserver(this);
    }


    void InvokeHealthChanged(float currentHealth,float maxHealth)
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void InvokeManaChanged(float currentMana, float maxMana)
    {
        OnManaChanged?.Invoke(currentMana, maxMana);
    }
}
