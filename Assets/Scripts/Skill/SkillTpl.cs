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
    private SOStatusTpl[] selfStatus;
    [SerializeField]
    private AttrLinkTpl[] selfHealing;
    [SerializeField]
    private SkillCollisionTpl[] collisions;

    public string Description { get => description; }
    public float Cooldown { get => cooldown; }
    public float ManaCost { get => manaCost;}
    public SOStatusTpl[] SelfStatus { get => selfStatus;  }
    public AttrLinkTpl[] SelfHealing { get => selfHealing;  }
    public SkillCollisionTpl[] Collisions { get => collisions; }
}
