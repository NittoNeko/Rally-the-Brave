using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusTpl
{
    [SerializeField, TextArea]
    private string description;
    [SerializeField]
    private EStatusType type;
    [SerializeField]
    private bool isRemovable;
    [SerializeField, MinValue(0)]
    private float initialTime;
    [SerializeField, MinValue(0),Tooltip("0 means it is a persistent status")]
    private float maxTime;
    [SerializeField, MinValue(0)]
    private int maxStack;
    [SerializeField]
    private AttrModifierTpl[] attrModifiers;
    [SerializeField]
    private PeriodicCombatResourceEffectTpl[] combatPeriodEffects;
    [SerializeField]
    private ESpecialEffectType[] specialStatuses;

    public string Description => description;

    public EStatusType Type => type;

    public bool IsPersistent => maxTime == 0;

    public bool IsRemovable => isRemovable;

    public float InitialTime => initialTime;

    public float MaxTime => maxTime;

    public int MaxStack => maxStack;

    public AttrModifierTpl[] AttrModifiers => attrModifiers;

    public ESpecialEffectType[] SpecialStatuses => specialStatuses;

    public PeriodicCombatResourceEffectTpl[] CombatPeriodEffects => combatPeriodEffects;
}
