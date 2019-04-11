using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle combat-related behaviors.
/// </summary>
public interface ICombater : ICombatHolder, IFactionHolder
{
    /// <summary>
    /// Triggered upon taking damage.
    /// Parameters: Actual Damage Taken.
    /// </summary>
    event System.Action<float> OnDamageTaken;

    /// <summary>
    /// Triggered upon taking damage.
    /// Parameters: Actual Healing Taken.
    /// </summary>
    event System.Action<float> OnHealTaken;

    /// <summary>
    /// Take certain damage with/out triggering events. May trigger death event.
    /// Return actual damage taken.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isPercent"></param>
    /// <param name="canDodge"></param>
    /// <param name="canImmune"></param>
    /// <param name="isTrigger"></param>
    float TakeDamage(float damage, bool isPercent, bool canDodge = true, bool canImmune = true, bool isTrigger = true);

    /// <summary>
    /// Take certain healing with/out triggering events.
    /// Return actual heal taken.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isPercent"></param>
    /// <param name="isTrigger"></param>
    float TakeHeal(float value, bool isPercent, bool isTrigger = true);

    /// <summary>
    /// Gain/Lose health by an offset. May trigger death event.
    /// Return actual health delta.
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="isPercent"></param>
    float ModifyHealth(float offset, bool isPercent);

    /// <summary>
    /// Gain/Lose mana by an offset.
    /// Return actual mana delta.
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="isPercent"
    float ModifyMana(float offset, bool isPercent);

    /// <summary>
    /// Kill this combater at once.
    /// If isForce, this will die anyway.
    /// Return true if dead, other false.
    /// </summary>
    /// <param name="isForce"></param>
    bool Die(bool isForce = false);
}
