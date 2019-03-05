using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger when vitality changes
public delegate void VitalityChange(Transform target, float previous, float current, float max);

// Trigger when primary resource changes
public delegate void PriResChange(Transform target, float previous, float current, float max);

// Trigger when a constant event happens
public delegate void CombatEvent(Transform target);

public interface ICharResHolder
{
    event PriResChange OnPriResChange;
    event VitalityChange OnVitalityChange;
    event CombatEvent OnDeath;

    float Vitality { get; set; }
    float PriRes { get; set; }
}
