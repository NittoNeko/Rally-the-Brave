using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatResourceEffectExecutor : ICombatResourceEffectExecutor
{
    private static ICombatResourceEffectExecutor instance;

    public static ICombatResourceEffectExecutor Instance
    {
        get
        {
            if (instance == null) {
                instance = new CombatResourceEffectExecutor();
            }

            return instance;
        }
    }

    /// <summary>
    /// Calculate the power of fixed power + linked power + percent power,
    /// and apply corresponding effects to the taker
    /// </summary>
    public void TakeEffect(CombatResourceEffectTpl combatResourceEffect, IAttrHolder applierAttr, ICombater taker, int multiplier = 1)
    {
        // get fixed power
        float _power = combatResourceEffect.FixedPower;

        // get percent power
        if (taker != null)
        {
            switch (combatResourceEffect.Type)
            {
                case ECombatResourceEffectType.HealthGain:
                case ECombatResourceEffectType.HealthLose:
                case ECombatResourceEffectType.Damage:
                    _power += combatResourceEffect.PercentPower * taker.MaxHealth;
                    break;
                case ECombatResourceEffectType.Heal:
                case ECombatResourceEffectType.ManaGain:
                case ECombatResourceEffectType.ManaLose:
                    _power += combatResourceEffect.PercentPower * taker.MaxMana;
                    break;
            }
        }

        // get linked power
        if (applierAttr != null)
        {
            for (int i = 0; i < combatResourceEffect.ApplierAttrLinks.Length; ++i)
            {
                float _attr = applierAttr.GetAttr(combatResourceEffect.ApplierAttrLinks[i].LinkedType);
                _power += _attr * combatResourceEffect.ApplierAttrLinks[i].Percent;
            }
        }

        // take multiplier
        _power = _power * Mathf.Max(0, multiplier);

        // take effects
        switch (combatResourceEffect.Type)
        {
            case ECombatResourceEffectType.HealthGain:
                taker.ModifyHealth(_power, false);
                break;
            case ECombatResourceEffectType.HealthLose:
                taker.ModifyHealth(-_power, false);
                break;
            case ECombatResourceEffectType.Damage:
                taker.TakeDamage(_power, false, canDodge: false);
                break;
            case ECombatResourceEffectType.Heal:
                taker.TakeHeal(_power, false);
                break;
            case ECombatResourceEffectType.ManaGain:
                taker.ModifyMana(_power, false);
                break;
            case ECombatResourceEffectType.ManaLose:
                taker.ModifyMana(-_power, false);
                break;
        }

    }


}
