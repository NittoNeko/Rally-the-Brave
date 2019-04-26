using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class CombatResourceEffectTpl
{
    [SerializeField]
    private ECombatResourceEffectType type;
    [SerializeField, MinValue(0)]
    private float fixedPower;
    [SerializeField, MinValue(0)]
    private float percentPower;
    [SerializeField]
    private AttrLinkTpl[] applierAttrLinks;

    public ECombatResourceEffectType Type => type;

    public AttrLinkTpl[] ApplierAttrLinks { get => applierAttrLinks; }
    public float FixedPower { get => fixedPower; }
    public float PercentPower { get => percentPower; }
}
