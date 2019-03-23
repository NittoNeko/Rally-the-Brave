using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttrModifierTpl
{
    [SerializeField]
    private EAttrModLayer layer;
    [SerializeField]
    private EAttrType attrType;
    [SerializeField]
    private float value;

    public EAttrModLayer Layer => layer;

    public EAttrType AttrType => attrType;

    public float Value => value;
}
