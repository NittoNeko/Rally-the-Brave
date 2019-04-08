using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatResourceMultiple", menuName = "ScriptableObject/CombatResourceMultiple")]
public class SOCombatResourceMultipleTpl : ScriptableObject
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
