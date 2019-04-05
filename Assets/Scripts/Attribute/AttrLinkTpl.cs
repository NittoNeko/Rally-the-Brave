using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[System.Serializable]
public class AttrLinkTpl
{
    [SerializeField, MinValue(0)]
    private float percent;
    [SerializeField]
    private EAttrType linkedType;

    public float Percent { get => percent; }
    public EAttrType LinkedType { get => linkedType; }
}