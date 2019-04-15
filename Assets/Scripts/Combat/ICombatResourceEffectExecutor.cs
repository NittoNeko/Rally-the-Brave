using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatResourceEffectExecutor
{
    void TakeEffect(CombatResourceEffectTpl combatResourceEffect, IAttrHolder applierAttr, ICombater taker, int multiplier = 1);
}
