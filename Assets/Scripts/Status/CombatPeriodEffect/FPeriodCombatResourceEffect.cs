using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class FPeriodCombatResourceEffect
{
    public static IPeriodicCombatResourceEffect Create(PeriodicCombatResourceEffectTpl[] source, ICombater taker, IAttrHolder applierAttr)
    {
        return new PeriodicCombatResourceEffect(source, taker, applierAttr, true);
    }

    public static PeriodicCombatResourceEffect CreateWithoutAttr(PeriodicCombatResourceEffectTpl[] source, ICombater taker)
    {
        return new PeriodicCombatResourceEffect(source, taker, null, false);
    }
}