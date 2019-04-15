using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class PeriodicCombatResourceEffectTpl
{
    [SerializeField, MinValue(0)]
    private float interval;
    [SerializeField, HideLabel]
    private CombatResourceEffectTpl combatEffect;

    public float Interval => interval;

    public CombatResourceEffectTpl CombatEffect { get => combatEffect;}
}


