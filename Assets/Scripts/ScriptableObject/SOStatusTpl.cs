using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObject/Status")]
public class SOStatusTpl : ScriptableObject
{
    [SerializeField, TextArea]
    private string description;
    [SerializeField]
    private EStatusType type;
    [SerializeField, Tooltip("Whether this status expires due to time.")]
    private bool canExpire;
    [SerializeField]
    private bool isRemovable;
    [SerializeField, MinValue(0)]
    private float initialTime;
    [SerializeField, MinValue(0)]
    private float maxTime;
    [SerializeField, MinValue(0)]
    private int maxStack;
    [SerializeField]
    private AttrModifierTpl[] attrModifiers;
    [SerializeField]
    private FixedPeriodStatusTpl[] fixedPeriod;
    [SerializeField]
    private LinkedPeriodStatusTpl[] linkedPeriod;
    [SerializeField]
    private ESpecialStatusType[] specialStatus;

    public string Description => description;

    public EStatusType Type => type;

    public bool CanExpire => canExpire;

    public bool IsRemovable => isRemovable;

    public float InitialTime => initialTime;

    public float MaxTime => maxTime;

    public int MaxStack => maxStack;

    public AttrModifierTpl[] AttrModifiers => attrModifiers;

    public ESpecialStatusType[] SpecialStatus => specialStatus;

    public FixedPeriodStatusTpl[] FixedPeriod => fixedPeriod;

    public LinkedPeriodStatusTpl[] LinkedPeriod => linkedPeriod;
}


