using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatResourceHolder
{
    /// <summary>
    /// Triggered when health changes.
    /// Parameters: Previous Health, Current Health, MaxHealth.
    /// </summary>
    event System.Action<float, float, float> OnHealthChange;

    /// <summary>
    /// Triggered when health changes.
    /// Parameters: Previous Mana, Current Mana, MaxMana.
    /// </summary>
    event System.Action<float, float, float> OnManaChange;

    float Health { get; }
    float Mana { get; }
    float MaxHealth { get; }
    float MaxMana { get; }
}
