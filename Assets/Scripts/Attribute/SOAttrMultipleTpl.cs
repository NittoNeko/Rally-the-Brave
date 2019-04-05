using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeMultiplePreset", menuName = "ScriptableObject/AttributeMultiplePreset")]
public class SOAttrMultipleTpl : ScriptableObject
{
    // how is vitality converted
    [SerializeField, MinValue(0)]
    private int vitalityMultiple;

    // how is spirit converted
    [SerializeField, MinValue(0)]
    private int spiritMultiple;

    public int VitalityMultiple => vitalityMultiple;
    public int SpiritMultiple => spiritMultiple;
}
