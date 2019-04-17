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
    [SerializeField]
    private ESkillMoveType moveType;
    [SerializeField]
    private CombatResourceEffectTpl[] effectOnCollision;
    // 0 means it will always pierce
    [SerializeField, Tooltip("how many targets this effect can hit per interval. 0 means infinite."), MinValue(0)]
    private int hitCount;
    [SerializeField, Tooltip("can it hit through")]
    private bool isSpectual;

}
