using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data collection of character attribute modifiers
/// </summary>
[System.Serializable]
public class AttrModifier
{
    [SerializeField]
    private readonly EAttrModLayer layer;
    [SerializeField]
    private readonly EAttrType attrType;
    [SerializeField]
    private readonly float value;
    [SerializeField]
    private readonly bool canStack;

    public float Value { get => value;}
    public EAttrType AttrType { get => attrType; }
    public EAttrModLayer Layer => layer;
    public bool CanStack => canStack;
}
