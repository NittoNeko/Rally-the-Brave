using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObject/Status")]
public class SOStatus : ScriptableObject
{
    [SerializeField]
    private bool isTickable = true; // If a status is tickable, it will expire due to time.
    [SerializeField, Range(0, ushort.MaxValue)]
    private readonly float maxTime;
    [SerializeField, Range(0, ushort.MaxValue)]
    private readonly float initialTime;
    [SerializeField]
    private readonly ushort maxStack;
    [SerializeField]
    private readonly string description;
    [SerializeField]
    private readonly EStatusType type;

    [SerializeField]
    private readonly AttrModifier[] attrModifiers;

    [SerializeField]
    private readonly float[] vitOverTime;
    [SerializeField]
    private readonly float[] priResOverTime;

    [SerializeField]
    private readonly ESpecialStatusType[] specialStatus;

    public float MaxTime => maxTime;

    public float InitialTime => initialTime;

    public ushort MaxStack => maxStack;

    public string Description => description;

    public EStatusType Type => type;

    public AttrModifier[] AttrModifiers => attrModifiers;

    public float[] VitOverTime => vitOverTime;

    public float[] PriResOverTime => priResOverTime;

    public ESpecialStatusType[] SpecialStatus => specialStatus;
}


