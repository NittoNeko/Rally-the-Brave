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
    private ICombatResourceEffectExecutor effectExecutor;

    public int Stack { get => stack; set => stack = value; }

    public PeriodicCombatResourceEffect(PeriodicCombatResourceEffectTpl[] source, ICombater taker, IAttrHolder applierAttr, ICombatResourceEffectExecutor effectExecutor)
    {
        this.effects = new TimedEffect[source.Length];
        for (int i = 0; i < source.Length; ++i)
        {
            this.effects[i] = new TimedEffect(source[i]);
        }

        this.effectExecutor = effectExecutor;
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

                // take effects
                effectExecutor.TakeEffect(_effect.source.CombatEffect, applierAttr, taker, stack);
            }
        }
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
