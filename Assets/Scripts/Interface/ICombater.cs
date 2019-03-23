using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamageEvent(float value);
public delegate void HealEvent(float value);

/// <summary>
/// Handle combat-related behaviors.
/// </summary>
public interface ICombater
{
    event DamageEvent OnDamageTaken;
    event HealEvent OnHealTaken;

    /// <summary>
    /// Take certain damage reduced by armor with/out triggering events. May trigger death event.
    /// Return actual damage taken.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isTrigger"></param>
    float TakeDamage(float value, bool isPercent, bool isTrigger = true);

    /// <summary>
    /// Take certain healing with/out triggering events.
    /// Return actual heal taken.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isTrigger"></param>
    float TakeHeal(float value, bool isPercent, bool isTrigger = true);

    /// <summary>
    /// Gain/Lose health by an offset. May trigger death event.
    /// Return actual health delta.
    /// </summary>
    /// <param name="offset"></param>
    float ModifyHealth(float offset, bool isPercent);

    /// <summary>
    /// Gain/Lose mana by an offset.
    /// Return actual mana delta.
    /// </summary>
    /// <param name="offset"></param>
    float ModifyMana(float offset, bool isPercent);
}
