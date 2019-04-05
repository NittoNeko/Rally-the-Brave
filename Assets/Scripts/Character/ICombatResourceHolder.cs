using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChange(float previous, float current, float max);
public delegate void ManaChange(float previous, float current, float max);


public interface ICombatHolder
{
    // triggered when health changes
    event HealthChange OnHealthChange;

    // triggered when mana changes
    event ManaChange OnManaChange;

    float Health { get; }
    float Mana { get; }
    float MaxHealth { get; }
    float MaxMana { get; }
}
