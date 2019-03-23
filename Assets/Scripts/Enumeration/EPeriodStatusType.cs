using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPeriodStatusType
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

