using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SkillCollisionTpl
{
    [SerializeField]
    private GameObject collision;
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name")]
    private CombatResourceEffectTplWrapper[] combatEffects;
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name")]
    private SOStatusTplWrapper[] statuses;

    [SerializeField]
    private CombatResourceEffectTpl combatEffectToEnemy;
    [SerializeField]
    private CombatResourceEffectTpl combatEffectToAlly;


    public SkillCollisionTpl()
    {
        this.combatEffects = new CombatResourceEffectTplWrapper[EnumArray.CombatererRelationship.Length];
        this.statuses = new SOStatusTplWrapper[EnumArray.CombatererRelationship.Length];

        for (int i = 0; i < EnumArray.CombatererRelationshipName.Length; ++i)
        {
            this.combatEffects[i] = new CombatResourceEffectTplWrapper(EnumArray.CombatererRelationshipName[i]);
            this.statuses[i] = new SOStatusTplWrapper(EnumArray.CombatererRelationshipName[i]);
        }
    }

    [System.Serializable]
    private class CombatResourceEffectTplWrapper
    {
        [SerializeField, HideInInspector]
        private string name;
        [SerializeField]
        private CombatResourceEffectTpl[] value;

        public CombatResourceEffectTplWrapper(string name)
        {
            this.name = name;
        }
    }

    [System.Serializable]
    private class SOStatusTplWrapper
    {
        [SerializeField, HideInInspector]
        private string name;
        [SerializeField]
        private SOStatusTplWrapper[] value;

        public SOStatusTplWrapper(string name)
        {
            this.name = name;
        }
    }
}
