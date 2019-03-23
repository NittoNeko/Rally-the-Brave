using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;



/// <summary>
/// - Set, reset and refresh attributes.
/// - Apply attribute modifiers.
/// </summary>
public class MBAttrMgr : MonoBehaviour, IAttrModifiable
{
    // triggered when an attribute changes
    public event AttrChange OnAttrChange;

    // attributes and their modifier
    private Attributes[] attributes;

    // fixed base, maximum and minimum attributes
    [SerializeField]
    private SOAttrTpl attrPreset;

    private void Awake()
    {
        // initialize attribute containers
        attributes = new Attributes[EnumArray.AttrType.Length];
        for (int i = 0; i < attributes.Length; ++i)
        {
            attributes[i] = new Attributes();
        }

        // get all attributes ready
        RefreshAll(false);
    }


    public float GetAttr(EAttrType type)
    {
        return attributes[(int)type].ActualValue;
    }

    /// <summary>
    /// <see cref="IAttrModifiable.TakeModifier(AttrModifierTpl, bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void TakeModifier(AttrModifierTpl attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, true);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RemoveModifier(AttrModifierTpl, bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RemoveModifier(AttrModifierTpl attrModifier, bool isRefresh = true)
    {
        ChangeModifiers(attrModifier, false);
        if (isRefresh) RecalculateAttr(attrModifier.AttrType);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.TakeModifier(AttrModifierTpl[], bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void TakeModifier(AttrModifierTpl[] attrModifier, bool isRefresh = true)
    {
        foreach(AttrModifierTpl _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, true);
        }
        if (isRefresh) RefreshAll(true);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RemoveModifier(AttrModifierTpl[], bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RemoveModifier(AttrModifierTpl[] attrModifier, bool isRefresh = true)
    {
        foreach (AttrModifierTpl _modifier in attrModifier)
        {
            ChangeModifiers(_modifier, false);
        }
        if (isRefresh) RefreshAll(true);
    }

    /// <summary>
    /// <see cref="IAttrModifiable.RefreshAll(bool)"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void RefreshAll(bool isDirtyOnly)
    {
        for (int i = 0; i < attribute.Length; ++i)
        {
            Attributes _attr = attribute[i];
            if (!isDirtyOnly || _attr.IsModDirty == true)
            {
                RecalculateAttr((EAttrType)i);
            }
        }
    }

    /// <summary>
    /// <see cref="IAttrModifiable.ResetAll"/>
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isRefresh"></param>
    public void ResetAll()
    {
        foreach(Attributes _charAttr in attribute)
        {
            // reset multiplicative modifiers back to 1
            for(int i = 0; i < _charAttr.Modifiers.Length; ++i)
            {
                _charAttr.Modifiers[i] = 1;
            }

            // reset additive modifiers back to 0
            _charAttr.Modifiers[(int)EAttrModLayer.Additive] = 0;

            _charAttr.independent.Clear();
        }
    }

    /// <summary>
    /// Add or remove modifier.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isAdd"></param>
    private void ChangeModifiers(AttrModifierTpl attrModifier, bool isAdd)
    {
        Attributes _charAttr = attribute[((int)attrModifier.AttrType)];

        // as for independent, we should keep its track first
        //   then recalculate its caches.
        if (attrModifier.Layer == (int)EAttrModLayer.Independent)
        {
            if (isAdd)
            {
                _charAttr.independent.Add(attrModifier.Value);
            }
            else
            {
                _charAttr.independent.Remove(attrModifier.Value);
            }

            // recalculate caches
            float _result = 1;
            foreach (float _value in _charAttr.independent)
            {
                _result *= _value;
            }
            _charAttr.Modifiers[(int)attrModifier.Layer] = _result;
        }
        else
        {
            _charAttr.Modifiers[(int)attrModifier.Layer] += isAdd ? attrModifier.Value : -attrModifier.Value;
        }

        // set dirty
        _charAttr.SetDirty();
    }

    /// <summary>
    /// Recalculate a certain attribute.
    /// </summary>
    /// <param name="type"></param>
    private void RecalculateAttr(EAttrType type)
    {
        float _previous = GetAttr(type);

        attributes[(int)type].Recalculate(attrPreset.GetBase(type), attrPreset.GetMax(type), attrPreset.GetMin(type));

        // trigger attribute change event
        OnAttrChange?.Invoke(type, _previous, GetAttr(type));
    }

    /// <summary>
    /// D
    /// </summary>
    private class Attributes
    {
        private static readonly int indSize = 2;
        private bool isModDirty;
        // caches of modifiers
        private float[] modifiers;
        // keep track of independent multiplicative to avoid imprecision
        private List<float> independent;
        private float actualValue;

        public bool IsModDirty => isModDirty;

        public float[] Modifiers => modifiers;

        public float ActualValue => actualValue;

        public Attributes()
        {
            isModDirty = true;
            modifiers = new float[EnumArray.AttrModLayer.Length];
            independent = new List<float>(indSize);
        }

        public void SetDirty()
        {
            isModDirty = true;
        }

        public void Recalculate(float initial, float max, float min)
        {
            // get additive modification
            float _additive = modifiers[(int)EAttrModLayer.Additive];

            // initial multiplicative should be 1
            float _multiplicative = 1;

            // calculate multiplicative modification
            for (int i = 0; i < modifiers.Length; ++i)
            {
                if (i != (int)EAttrModLayer.Additive)
                {
                    _multiplicative *= modifiers[i];
                }
            }

            // reset dirty flag
            isModDirty = true;

            // calculate actual value
            float _result = (initial + _additive) * _multiplicative;

            // range check
            actualValue = Mathf.Max(Mathf.Min(_result, max), min);
        }
    }
}

