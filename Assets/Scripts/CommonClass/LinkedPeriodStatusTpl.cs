using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class LinkedPeriodStatusTpl
{
    [SerializeField, MinValue(0)]
    private float interval;
    [SerializeField, MinValue(0)]
    private float percent;
    [SerializeField]
    private EAttrType linkedType;
    [SerializeField]
    private EPeriodStatusType type;

    public float Interval => interval;

    public float Percent => percent;

    public EAttrType LinkedType => linkedType;

    public EPeriodStatusType Type => type;
}
