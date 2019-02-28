using System;
using System.Collections.Generic;
using UnityEngine;

// A generic attribute holder
public class MBCharAttr : MonoBehaviour, IAttrCollection, IAttrModifiable
{
    public event AttrChange OnAttrChange;

    // Fixed base, maximum and minimum attributes
    [SerializeField]
    private readonly SOCharAttrPreset baseAttr;
    [SerializeField]
    private readonly SOCharAttrPreset maxAttr;
    [SerializeField]
    private readonly SOCharAttrPreset minAttr;

    // Actual attributes after all modifications
    private readonly CharAttr actualAttr = new CharAttr(0);

    // Reference to all components that may change attributes
    private IAttrModifier[] attrModifiers;

    // Avoid duplicate initialization of array
    private float[] modHolder = new float[EnumArray.AttrModLayerCount];

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

    private void OnEnable()
    {
        foreach(IAttrModifier attrModifier in attrModifiers)
        {
            attrModifier.OnAttrModChange += RecalculateAttr;
        }
    }

    private void OnDisable()
    {
        foreach (IAttrModifier attrModifier in attrModifiers)
        {
            attrModifier.OnAttrModChange -= RecalculateAttr;
        }
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

    public bool ExistAttr(EAttrType type)
    {
        return CharAttr.ExistAttr(type);
    }


    // Recalculate attributes upon changes like wearing equipment
    private void RecalculateAttr(EAttrType type)
    {
        // Reset all modification to 0
        for (int i = 0; i < modHolder.Length; ++i)
        {
            modHolder[i] = 0;
        }

        // Base multiplicative should be 1
        modHolder[(int)EAttrModLayer.Independent] = 1;

        foreach (IAttrModifier attrModifier in attrModifiers)
        {
            float[] mods = attrModifier.GetModifiers(type);
            for(int i = 0; i < modHolder.Length; ++i)
            {
                if (i == (int)EAttrModLayer.Independent)
                {
                    modHolder[i] *= mods[i];
                } else
                {
                    modHolder[i] += mods[i];
                }
            }
        }

        // Get final additive modification
        float additive = modHolder[(int)EAttrModLayer.Additive];
        float multiplicative = 1;

        // Calculate final multiple
        for (int i = 0; i < modHolder.Length; ++i)
        {
            if (i != (int) EAttrModLayer.Additive)
            {
                multiplicative *= modHolder[i];
            }
        }

        // Calculate final result
        float result = (baseAttr.Get(type) + additive) * multiplicative;
        SetAttr(type, result);
    }

    // Data collection
    public class CharAttr {
        private float maxVitality;
        private float vitalityRec; // vitality gain per second
        private float vitalitySteal; // vitality gain per hit
        private float maxPriRes;
        private float priResRec; // primary resource gain per second
        private float priResSteal; // primary resource gain per hit
        private float attack;
        private float haste; // percentage
        private float critRate; // percentage
        private float critMult; // percentage
        private float focus; // percentage
        private float armor; // percentage
        private float thorn; // damage return per hit
        private float moveSpeed; // game unit

        public CharAttr(float allValue)
            : this(allValue, allValue, allValue, allValue, allValue, allValue, allValue,
                  allValue, allValue, allValue, allValue, allValue, allValue, allValue) {}

        public CharAttr(float maxVitality, float vitalityRec, float vitalitySteal, float maxPriRes, 
            float priResRec, float priResSteal, float attack, float haste, float critRate, float critMult, 
            float focus, float armor, float thorn, float moveSpeed)
        {
            this.maxVitality = maxVitality;
            this.vitalityRec = vitalityRec;
            this.vitalitySteal = vitalitySteal;
            this.maxPriRes = maxPriRes;
            this.priResRec = priResRec;
            this.priResSteal = priResSteal;
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
                case EAttrType.MaxVitality:
                case EAttrType.VitalityRec:
                case EAttrType.VitalitySteal:
                case EAttrType.MaxPriRes:
                case EAttrType.PriResRec:
                case EAttrType.PriResSteal:
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
                case EAttrType.MaxVitality:
                    maxVitality = value;
                    break;
                case EAttrType.VitalityRec:
                    vitalityRec = value;
                    break;
                case EAttrType.VitalitySteal:
                    vitalitySteal = value;
                    break;
                case EAttrType.MaxPriRes:
                    maxPriRes = value;
                    break;
                case EAttrType.PriResRec:
                    priResRec = value;
                    break;
                case EAttrType.PriResSteal:
                    priResSteal = value;
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
                case EAttrType.MaxVitality:
                    return maxVitality;
                case EAttrType.VitalityRec:
                    return vitalityRec;
                case EAttrType.VitalitySteal:
                    return vitalitySteal;
                case EAttrType.MaxPriRes:
                    return maxPriRes;
                case EAttrType.PriResRec:
                    return priResRec;
                case EAttrType.PriResSteal:
                    return priResSteal;
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


    // SOCharAttrPreset is meant to work with MBCharAttr and GenericAttrSet
    // This is a data container for pre-defined attributes
    [CreateAssetMenu(fileName = "CharAttrPreset", menuName = "ScriptableObject/GenericAttrPreset")]
    private class SOCharAttrPreset : ScriptableObject
    {
        [SerializeField]
        private readonly CharAttr attr;

        public float Get(EAttrType type)
        {
            return attr.Get(type);
        }
    }
}

