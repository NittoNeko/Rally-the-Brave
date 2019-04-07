using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttrModifierTpl
{
    [SerializeField]
    private EAttrModifierLayer layer;
    [SerializeField]
    private EAttrType attrType;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float minValue;

    public EAttrModifierLayer Layer => layer;

    public EAttrType AttrType => attrType;

    public float MaxValue { get => maxValue; }
    public float MinValue { get => minValue; }
}
