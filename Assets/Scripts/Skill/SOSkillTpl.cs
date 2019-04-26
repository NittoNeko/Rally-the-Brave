using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPreset", menuName = "ScriptableObject/SkillPreset")]
public class SOSkillTpl : ScriptableObject
{
    [SerializeField, HideLabel]
    private SkillTpl skillTpl;
}
