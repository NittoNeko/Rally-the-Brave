using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class PeriodicCombatResourceEffectTpl
{
    [SerializeField, MinValue(0)]
    private float interval;
    [SerializeField, MinValue(0)]
    private float fixedPower;
    [SerializeField, MinValue(0)]
    private float percentPower;
    [SerializeField]
    private EPeriodicCombatResourceEffectType type;
    [SerializeField]
    private AttrLinkTpl[] applierAttrLinks;

    public float Interval => interval;

    public EPeriodicCombatResourceEffectType Type => type;

    public AttrLinkTpl[] ApplierAttrLinks { get => applierAttrLinks; }
    public float FixedPower { get => fixedPower; }
    public float PercentPower { get => percentPower; }
}


