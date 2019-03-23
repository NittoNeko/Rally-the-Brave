using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChange(float previous, float current, float max);

public delegate void ManaChange(float previous, float current, float max);

public class MBCombatInfo : MonoBehaviour
{

    public delegate void ManaChange(float previous, float current, float max);

    public delegate void AttrChange(EAttrType type, float previous, float current);

    // triggered when health changes
    public event HealthChange OnHealthChange;

    // triggered when mana changes
    public event ManaChange OnManaChange;

    // every character should have health
    private float health;

    // resources used to cast spells
    private float mana;

    // max health
    private float maxHealth;

    // max mana
    private float maxMana;

    // determine if dead
    private bool isAlive;

    public float Health
    {
        get => health;
        set
        {
            float _previous = Health;

            // range check
            health = Mathf.Max(Mathf.Min(value, MaxHealth), 0);

            // trigger health change event
            OnHealthChange?.Invoke(_previous, Health, MaxHealth);
        }
    }

    public float Mana
    {
        get => mana;
        set
        {
            float _previous = Mana;

            // range check
            mana = Mathf.Max(Mathf.Min(value, MaxMana), 0);

            // trigger mana change event
            OnManaChange?.Invoke(_previous, Mana, MaxMana);
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            Health = Health;
        }
    }
    public float MaxMana
    {
        get => maxMana;
        set
        {
            maxMana = value;
            Mana = Mana;
        }
    }

    public bool IsAlive { get => isAlive; set => isAlive = value; }
}
