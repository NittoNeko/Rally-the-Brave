using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SkillCollisionTpl
{
    [SerializeField]
    private GameObject collision;
    [SerializeField]
    private CombatResourceEffectTpl[] combatEffectToEnemy;
    [SerializeField]
    private CombatResourceEffectTpl[] combatEffectToAlly;
    [SerializeField]
    private SOStatusTpl[] statusToAlly;
    [SerializeField]
    private SOStatusTpl[] statusToEnemy;
}
