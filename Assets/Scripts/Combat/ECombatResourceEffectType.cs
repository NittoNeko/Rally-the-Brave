using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECombatResourceEffectType
{
    // health offset ignoring armor
    HealthGain,
    HealthLose,
    // health offset reduced by armor
    Damage,
    // healing offset
    Heal,
    // mana offset
    ManaGain,
    ManaLose
}
