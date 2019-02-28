using System;
using System.Collections.Generic;
using UnityEngine;

// A generic attribute holder
public class MBGenericAttr : MonoBehaviour, IAttrCollection, IAttrModifiable
{
    public event AttrChange OnAttrChange;

    // Fixed base, maximum and minimum attributes
    [SerializeField]
    private readonly SOGenericAttrPreset baseAttr;
    [SerializeField]
    private readonly SOGenericAttrPreset maxAttr;
    [SerializeField]
    private readonly SOGenericAttrPreset minAttr;

    // Actual attributes after all modifications
    private readonly GenericAttr actualAttr = new GenericAttr(0);

    // Reference to all components that may change attributes
    private IAttrModifier[] attrModifiers;

    public void RefreshAllAttr()
    {
        foreach (EAttrType type in EnumArray.AttrType)
        {
            if (ExistAttr(type))
            {
                RecalculateAttr(type);
            }
        }
    }

    // Return a attribute of type
    public float GetAttr(EAttrType type)
    {
        return actualAttr.Get(type);
    }

    private void Awake()
    {
        // Get reference to all attribute modifiers
        attrModifiers = GetComponents<IAttrModifier>();

        // Refresh all attributes
        RefreshAllAttr();

    }

    // This function would do range checking and event handling
    // Most time this function should be called
    private void SetAttr(EAttrType type, float value)
    {
        float previous = GetAttr(type);
        float upper = maxAttr.Get(type);
        float lower = minAttr.Get(type);
        float actual = value;

        // Range check
        if (actual > upper)
        {
            actual = upper;
        }
        else if (actual <= lower)
        {
            actual = lower;
        }

        actualAttr.Set(type, actual);

        // Trigger specific events
        if (previous != actual)
        {
            OnAttrChange(transform, type, previous, actual);
        }
    }



    // Recalculate attributes upon changes like wearing equipment
    private void RecalculateAttr(EAttrType type)
    {
        foreach (IAttrModifier attrModifier in attrModifiers)
        {
            foreach (AttrModifier modifier in attrModifier.GetModifiers(type))
            {
                
            }
        }


    }

    public bool ExistAttr(EAttrType type)
    {
        return GenericAttr.ExistAttr(type);
    }



    // Data collection
    public class GenericAttr {
        private float maxHealth;
        private float healthRec; // health gain per second
        private float healthSteal; // health gain per hit
        private float maxMana;
        private float manaRec; // mana gain per second
        private float manaSteal; // mana gain per hit
        private float attack;
        private float haste; // percentage
        private float critRate; // percentage
        private float critMult; // percentage
        private float focus; // percentage
        private float armor; // percentage
        private float thorn; // damage return per hit
        private float moveSpeed; // game unit

        public GenericAttr(float allValue)
            : this(allValue, allValue, allValue, allValue, allValue, allValue, allValue,
                  allValue, allValue, allValue, allValue, allValue, allValue, allValue) {}

        public GenericAttr(float maxHealth, float healthRec, float healthSteal, float maxMana, 
            float manaRec, float manaSteal, float attack, float haste, float critRate, float critMult, 
            float focus, float armor, float thorn, float moveSpeed)
        {
            this.maxHealth = maxHealth;
            this.healthRec = healthRec;
            this.healthSteal = healthSteal;
            this.maxMana = maxMana;
            this.manaRec = manaRec;
            this.manaSteal = manaSteal;
            this.attack = attack;
            this.haste = haste;
            this.critRate = critRate;
            this.critMult = critMult;
            this.focus = focus;
            this.armor = armor;
            this.thorn = thorn;
            this.moveSpeed = moveSpeed;
        }

        public static bool ExistAttr(EAttrType type)
        {
            switch (type)
            {
                case EAttrType.MaxHealth:
                case EAttrType.HealthRec:
                case EAttrType.HealthSteal:
                case EAttrType.MaxMana:
                case EAttrType.ManaRec:
                case EAttrType.ManaSteal:
                case EAttrType.Attack:
                case EAttrType.Haste:
                case EAttrType.CritRate:
                case EAttrType.CritMult:
                case EAttrType.Focus:
                case EAttrType.Armor:
                case EAttrType.Thorn:
                case EAttrType.MoveSpeed:
                    return true;
                default:
                    return false;
            }
        }


        // Modify a attribute based on its type
        public void Set(EAttrType type, float value)
        {
            switch (type)
            {
                case EAttrType.MaxHealth:
                    maxHealth = value;
                    break;
                case EAttrType.HealthRec:
                    healthRec = value;
                    break;
                case EAttrType.HealthSteal:
                    healthSteal = value;
                    break;
                case EAttrType.MaxMana:
                    maxMana = value;
                    break;
                case EAttrType.ManaRec:
                    manaRec = value;
                    break;
                case EAttrType.ManaSteal:
                    manaSteal = value;
                    break;
                case EAttrType.Attack:
                    attack = value;
                    break;
                case EAttrType.Haste:
                    haste = value;
                    break;
                case EAttrType.CritRate:
                    critRate = value;
                    break;
                case EAttrType.CritMult:
                    critMult = value;
                    break;
                case EAttrType.Focus:
                    focus = value;
                    break;
                case EAttrType.Armor:
                    armor = value;
                    break;
                case EAttrType.Thorn:
                    thorn = value;
                    break;
                case EAttrType.MoveSpeed:
                    moveSpeed = value;
                    break;
                default:
                    Reporter.AttrMissing(type);
                    break;

            }
        }

        // Retrieve float from EAttrType
        public float Get(EAttrType type)
        {
            switch (type)
            {
                case EAttrType.MaxHealth:
                    return maxHealth;
                case EAttrType.HealthRec:
                    return healthRec;
                case EAttrType.HealthSteal:
                    return healthSteal;
                case EAttrType.MaxMana:
                    return maxMana;
                case EAttrType.ManaRec:
                    return manaRec;
                case EAttrType.ManaSteal:
                    return manaSteal;
                case EAttrType.Attack:
                    return attack;
                case EAttrType.Haste:
                    return haste;
                case EAttrType.CritRate:
                    return critRate;
                case EAttrType.CritMult:
                    return critMult;
                case EAttrType.Focus:
                    return focus;
                case EAttrType.Armor:
                    return armor;
                case EAttrType.Thorn:
                    return thorn;
                case EAttrType.MoveSpeed:
                    return moveSpeed;
                default:
                    Reporter.AttrMissing(type);
                    return default;
            }
        }
    }


    // SOGenericAttrPreset is meant to work with MBGenericAttr and GenericAttrSet
    // This is a data container for pre-defined attributes
    [CreateAssetMenu(fileName = "GenericAttrPreset", menuName = "ScriptableObject/GenericAttrPreset")]
    private class SOGenericAttrPreset : ScriptableObject
    {
        [SerializeField]
        private readonly GenericAttr attr;

        public float Get(EAttrType type)
        {
            return attr.Get(type);
        }
    }
}

