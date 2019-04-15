using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SkillTpl
{
    [SerializeField, TextArea]
    private string description;
    [SerializeField, MinValue(0)]
    private float cooldown;
    [SerializeField]
    private float manaCost;
    [SerializeField]
    private CombatResourceEffectTpl[] directEffects;

    public string Description { get => description; }
    public float Cooldown { get => cooldown; }
    public float ManaCost { get => manaCost;}
    public CombatResourceEffectTpl[] DirectEffects { get => directEffects; }
}
