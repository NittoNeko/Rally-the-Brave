using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle combat-related events like TakeDamage, OnDeath and so on.
/// </summary>
public class MBCombatMgr : SerializedMonoBehaviour, ICombater, ICombatRevivable
{
    public event System.Action<float, float, float> OnHealthChange;
    public event System.Action<float, float, float> OnManaChange;
    public event System.Action<float> OnDamageTaken;
    public event System.Action<float> OnHealTaken;

    [SerializeField, Required("SOAttrMultipleTpl is missing")]
    private SOCombatResourceMultipleTpl attrMultiple;

    [SerializeField, Required("IAttrHolder is missing.")]
    private IAttrHolder attrHolder;

    [SerializeField, Required("IDespawnable is missing.")]
    private IDespawnable despawner;

    // list of revive event listeners
    private List<ICombatReviver> combatRevivers = new List<ICombatReviver>(3);

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
        private set
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
        private set
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
        private set
        {
            maxHealth = value;
            Health = Health;
        }
    }
    public float MaxMana
    {
        get => maxMana;
        private set
        {
            maxMana = value;
            Mana = Mana;
        }
    }

    public bool IsAlive => isAlive;


    private void OnEnable()
    {
        attrHolder.OnAttrChange += CalculateMaxValue;
    }

    private void OnDisable()
    {
        attrHolder.OnAttrChange -= CalculateMaxValue;
    }

    public void AddReviver(ICombatReviver reviver)
    {
        combatRevivers.Add(reviver);
    }

    public void RemoveReviver(ICombatReviver reviver)
    {
        combatRevivers.Remove(reviver);
    }

    /// <summary>
    /// <see cref="ICombater.TakeDamage(float, bool, bool, bool, bool)"/>
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isPercent"></param>
    /// <param name="canDodge"></param>
    /// <param name="canImmune"></param>
    /// <param name="isTrigger"></param>
    public float TakeDamage(float damage, bool isPercent, bool canDodge = true, bool canImmune = true, bool isTrigger = true)
    {
        // return 0 if dead
        if (!IsAlive) return 0;

        // avoid damage if dodge
        if (canDodge && RNG.IsHit(attrHolder.GetAttr(EAttrType.Dodge)))
        {
            return 0;
        }

        // minimum damage is 0
        float _damage = Mathf.Max(damage, 0);

        // calculate actual damage
        float _result = _damage / (1 - attrHolder.GetAttr(EAttrType.Armor));

        // previous health before taking damage
        float _actual = Health;

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
        if (!IsAlive) return 0;

        // previous health before taking healing
        float _actual = Health;

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
        if (!IsAlive) return 0;

        float _delta = Health;

        // multiply _damage if isPercent
        if (isPercent)
        {
            offset = offset * MaxHealth;
        }

        // modify health by an offset
        Health += offset;

        _delta = Health - _delta;

        // call die when health is below 0
        if (Health <= 0)
        {
            Die();
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
        if (!IsAlive) return 0;

        float _delta = Mana;

        // multiply _damage if isPercent
        if (isPercent)
        {
            offset = offset * MaxMana;
        }

        _delta = Health - _delta;

        // modify health by an offset
        Mana += offset;

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

    /// <summary>
    /// <see cref="ICombater.Die(bool)"/>
    /// </summary>
    /// <param name="isForce"></param>
    /// <returns></returns>
    public bool Die(bool isForce = false)
    {
        // revived if reviver exists and not forced
        if (ConsumeReviver() && !isForce)
        {
            return false;
        }

        // set flag
        isAlive = false;

        // let despawner do the job
        despawner.Despawn();

        return true;
    }

    /// <summary>
    /// Recalculate max health and max mana if vitality and spirit changes.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="previous"></param>
    /// <param name="current"></param>
    private void CalculateMaxValue(EAttrType type, float previous, float current)
    {
        if (type == EAttrType.Vitality)
        {
            MaxHealth = current * attrMultiple.VitalityMultiple;
        }
        else if (type == EAttrType.Spirit)
        {
            MaxMana = current * attrMultiple.SpiritMultiple;
        }
    }
}
