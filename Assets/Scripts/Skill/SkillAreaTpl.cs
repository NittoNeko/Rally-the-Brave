using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SkillAreaTpl
{
    // 0 means it will never move
    [SerializeField, MinValue(0)]
    private float speed;
    [SerializeField]
    private ESkillMoveType moveType;
    [SerializeField]
    private CombatResourceEffectTpl[] effectOnCollision;
    [SerializeField, Tooltip("interval between effects. 0 means it never repeats."), MinValue(0)]
    private bool interval;
    [SerializeField, Tooltip("how long in seconds this effect lasts. 0 means it lasts until animation ends."), MinValue(0)]
    private float lasting;

}
