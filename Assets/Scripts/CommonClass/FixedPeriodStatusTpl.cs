using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class FixedPeriodStatusTpl
{
    [SerializeField, MinValue(0)]
    private float interval;
    [SerializeField]
    private bool isPercent;
    [SerializeField, MinValue(0)]
    private float power;
    [SerializeField]
    private EPeriodStatusType type;

    public float Interval => interval;

    public float Power => power;

    public bool IsPercent => isPercent;

    public EPeriodStatusType Type => type;
}
