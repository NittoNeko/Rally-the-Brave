using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SkillProjectileTpl
{
    // 0 means it will never move
    [SerializeField, MinValue(0)]
    private float speed;
    // 0 means it will always pierce

    [SerializeField]
    private ESkillMoveType moveType;
    [SerializeField]
    private CombatResourceEffectTpl[] effectOnCollision;
}
