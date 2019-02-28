using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttrModifier
{
    [SerializeField]
    private readonly EAttrModLayer layer;
    [SerializeField]
    private readonly EAttrType attrType;
    [SerializeField]
    private readonly float value;

    public float Value { get => value;}
    public EAttrType AttrType { get => attrType; }
    public EAttrModLayer Layer => layer;
}
