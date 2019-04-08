using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Effect that happens periodically and affects combater. 
/// This effect changes with taker and applier in real time.
/// </summary>
public class PeriodicCombatResourceEffect : IStatusStackable, IStatusUpdatable
{
    private TimedEffect[] effects;
    private IAttrHolder applierAttr;
    private ICombater taker;
    private int stack;
    private Func<PeriodicCombatResourceEffectTpl, float> GetAttrLinkedPower;

    public int Stack { get => stack; set => stack = value; }

    public PeriodicCombatResourceEffect(PeriodicCombatResourceEffectTpl[] source, ICombater taker, IAttrHolder applierAttr, bool isAttrLinked)
    {
        this.effects = new TimedEffect[source.Length];
        for (int i = 0; i < source.Length; ++i)
        {
            this.effects[i] = new TimedEffect(source[i]);
        }

        // if is attribute linked
        GetAttrLinkedPower = isAttrLinked ? (x) => { return 0; } : (Func<PeriodicCombatResourceEffectTpl, float>) CalculateLinked;
        this.taker = taker;
        this.applierAttr = applierAttr;
    }

    /// <summary>
    /// <see cref="IStatusUpdatable.OnUpdate"/>
    /// </summary>
    public void OnUpdate()
    {
        for (int i = 0; i < effects.Length; ++i)
        {
            TimedEffect _effect = effects[i];
            PeriodicCombatResourceEffectTpl _source = _effect.source;

            _effect.counter += Time.deltaTime;

            // interval reached
            if (_effect.counter >= _source.Interval)
            {
                // reset counter
                _effect.counter = 0;


                // snapshot of the total power at the moment
                float _power = CalculatePower(_effect.source) * stack;

                // take effects
                switch (_source.Type)
                {
                    case EPeriodicCombatResourceEffectType.HealthGain:
                        taker.ModifyHealth(_power, false);
                        break;
                    case EPeriodicCombatResourceEffectType.HealthLose:
                        taker.ModifyHealth(-_power, false);
                        break;
                    case EPeriodicCombatResourceEffectType.Damage:
                        taker.TakeDamage(_power, false, canDodge: false);
                        break;
                    case EPeriodicCombatResourceEffectType.Heal:
                        taker.TakeHeal(_power, false);
                        break;
                    case EPeriodicCombatResourceEffectType.ManaGain:
                        taker.ModifyMana(_power, false);
                        break;
                    case EPeriodicCombatResourceEffectType.ManaLose:
                        taker.ModifyMana(-_power, false);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Calculate the power of fixed power + linked power + percent power.
    /// </summary>
    private float CalculatePower(PeriodicCombatResourceEffectTpl source)
    {
        // get fixed power
        float _power = source.FixedPower;

        // get percent power
        switch (source.Type)
        {
            case EPeriodicCombatResourceEffectType.HealthGain:
            case EPeriodicCombatResourceEffectType.HealthLose:
            case EPeriodicCombatResourceEffectType.Damage:
                _power = source.PercentPower * taker.MaxHealth;
                break;
            case EPeriodicCombatResourceEffectType.Heal:
            case EPeriodicCombatResourceEffectType.ManaGain:
            case EPeriodicCombatResourceEffectType.ManaLose:
                _power = source.PercentPower * taker.MaxMana;
                break;
        }

        _power += GetAttrLinkedPower(source);

        return _power;
    }


    private float CalculateLinked(PeriodicCombatResourceEffectTpl source)
    {
        float _power = 0;
        for (int i = 0; i < source.ApplierAttrLinks.Length; ++i)
        {
            float _attr = applierAttr.GetAttr(source.ApplierAttrLinks[i].LinkedType);
            _power += _attr * source.ApplierAttrLinks[i].Percent;
        }
        return _power;
    }

    private struct TimedEffect
    {
        public float counter;
        public readonly PeriodicCombatResourceEffectTpl source;

        public TimedEffect(PeriodicCombatResourceEffectTpl source)
        {
            this.counter = 0;
            this.source = source;
        }
    }
}
