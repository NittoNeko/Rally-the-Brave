using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle combat-related events like TakeDamage, OnDeath and so on.
/// </summary>
public class MBCombatMgr : SerializedMonoBehaviour, ICombater, ICombatRevivable
{
    public event DamageEvent OnDamageTaken;
    public event HealEvent OnHealTaken;

    [SerializeField, Required("MBAttrHolder is missing.")]
    private MBAttrHolder attrHolder;

    [SerializeField, Required("MBCombatInfo is missing.")]
    private MBCombatInfo combatIndo;

    [SerializeField, Required("MBStatusHolder is missing.")]
    private MBStatusHolder statusHolder;

    [SerializeField, Required("IDespawnable is missing.")]
    private IDespawnable despawner;

    // list of revive event listeners
    private List<ICombatReviver> combatRevivers = new List<ICombatReviver>(3);

    public void AddReviver(ICombatReviver reviver)
    {
        combatRevivers.Add(reviver);
    }

    public void RemoveReviver(ICombatReviver reviver)
    {
        combatRevivers.Remove(reviver);
    }

    /// <summary>
    /// <see cref="ICombater.TakeDamage(float, bool, bool)"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isTrigger"></param>
    public float TakeDamage(float value, bool isPercent, bool isTrigger = true)
    {
        // return 0 if dead
        if (!combatIndo.IsAlive) return 0;

        // do not take damage if immortal
        if (statusHolder.SpecialStatus[(int)(ESpecialStatusType.Immortal)] > 0)
            return 0;

        // avoid damage if dodge
        if (RNG.IsHit(attrHolder.GetAttr(EAttrType.Dodge)))
        {
            return 0;
        }

        // minimum damage is 0
        float _damage = Mathf.Max(value, 0);

        // calculate actual damage
        float _result = _damage / attrHolder.GetAttr(EAttrType.Armor);

        // previous health before taking damage
        float _actual = combatIndo.Health;

        // decrease health
        _actual = ModifyHealth(-_result, isPercent);

        // range check for actual decreased health
        _actual = Mathf.Abs(Mathf.Min(_actual, 0));

        // trigger event if alive
        if (isTrigger) OnDamageTaken?.Invoke(_actual);

        return _actual;
    }

    /// <summary>
    /// <see cref="ICombater.TakeHeal(float, bool, bool)"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isTrigger"></param>
    public float TakeHeal(float value, bool isPercent, bool isTrigger = true)
    {
        // return 0 if dead
        if (!combatIndo.IsAlive) return 0;

        // previous health before taking healing
        float _actual = combatIndo.Health;

        // range check
        value = Mathf.Max(value, 0);

        // increase health
        _actual = ModifyHealth(value, isPercent);

        // range check for actual increased health
        _actual = Mathf.Max(_actual, 0);

        // trigger event if alive
        if (isTrigger) OnHealTaken?.Invoke(_actual);

        return _actual;
    }

    /// <summary>
    /// <see cref="ICombater.ModifyHealth(float, bool)"/>
    /// </summary>
    /// <param name="offset"></param>
    public float ModifyHealth(float offset, bool isPercent)
    {
        // return 0 if dead
        if (!combatIndo.IsAlive) return 0;

        float _delta = combatIndo.Health;

        // multiply _damage if isPercent
        if (isPercent)
        {
            offset = offset * combatIndo.MaxHealth;
        }

        // modify health by an offset
        combatIndo.Health += offset;

        _delta = combatIndo.Health - _delta;

        // try to revive if dead
        if (combatIndo.Health <= 0)
        {
            // dead if no reviver consumed
            if (!ConsumeReviver())
            {
                // let despawner do the job
                despawner.Despawn();
            }
        }

        return _delta;
    }

    /// <summary>
    /// <see cref="ICombater.ModifyMana(float, bool)"/>
    /// </summary>
    /// <param name="offset"></param>
    public float ModifyMana(float offset, bool isPercent)
    {
        // return 0 if dead
        if (!combatIndo.IsAlive) return 0;

        float _delta = combatIndo.Mana;

        // multiply _damage if isPercent
        if (isPercent)
        {
            offset = offset * combatIndo.MaxMana;
        }

        _delta = combatIndo.Health - _delta;

        // modify health by an offset
        combatIndo.Mana += offset;

        return _delta;
    }

    /// <summary>
    /// <see cref="ICombatRevivable.ConsumeReviver"/>
    /// </summary>
    public bool ConsumeReviver()
    {
        for (int i = 0; i < combatRevivers.Count; ++i)
        {
            if (combatRevivers[i].TryRevive())
            {
                // stop once a reviver is successful
                return true;
            }
        }

        return false;
    }
}
