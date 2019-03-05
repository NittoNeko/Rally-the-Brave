using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObject/Status")]
public class SOCharStatus : ScriptableObject
{
    [SerializeField]
    private readonly string description;
    [SerializeField]
    private readonly EStatusType type;
    [SerializeField, Tooltip("Whether this status expires due to time.")]
    private readonly bool canExpire = true;
    [SerializeField]
    private readonly bool isRemovable = true;
    [SerializeField, Min(0)]
    private readonly float initialTime;
    [SerializeField, Min(0)]
    private readonly float maxTime;
    [SerializeField, Min(0)]
    private readonly byte maxStack;
    [SerializeField]
    private readonly AttrModifier[] attrModifiers;
    [SerializeField, Tooltip("Damage Over Time affected by damage reduction")]
    private readonly float dmgOverTime;
    [SerializeField, Tooltip("Damage Over Time affected by damage reduction")]
    private readonly float vitOverTime;
    [SerializeField, Tooltip("Primary Resource Per Second")]
    private readonly float priResOverTime;
    [SerializeField]
    private readonly ESpecialStatusType[] specialStatus;

    public float MaxTime => maxTime;

    public float InitialTime => initialTime;

    public byte MaxStack => maxStack;

    public string Description => description;

    public EStatusType Type => type;

    public AttrModifier[] AttrModifiers => attrModifiers;
    public float DmgOverTime => dmgOverTime;

    public float VitOverTime => vitOverTime;

    public float PriResOverTime => priResOverTime;

    public ESpecialStatusType[] SpecialStatus => specialStatus;

    public bool CanExpire => canExpire;

    public bool IsRemovable => isRemovable;
}


