using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVitalityHolder
{
    event VitalityChange OnVitalityChange;
    event VitalityChange OnVitalityZero;
    event CombatEvent OnDeath;

    float Vitality { get; set; }
}
