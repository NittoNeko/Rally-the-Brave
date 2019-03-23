using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Data container for a character.
/// </summary>
public class MBAttrHolder : MonoBehaviour
{
    // triggered when an attribute changes
    public event AttrChange OnAttrChange;

    // fixed base, maximum and minimum attributes
    [SerializeField]
    private SOAttrTpl presetAttr;

    // final attributes
    private float[] attr;

    public SOAttrTpl PresetAttr => presetAttr;

    private void Awake()
    {
        attr = new float[EnumArray.AttrType.Length];
    }

    public float GetAttr(EAttrType type)
    {
        return attr[(int)type];
    }

    public void SetAttr(EAttrType type, float value)
    {
        float _max = PresetAttr.GetMax(type);
        float _min = PresetAttr.GetMin(type);
        float _previous = attr[(int)type];
        float result = Mathf.Max(Mathf.Min(value, _max), _min); // range check

        // assign result to attribute
        attr[(int)type] = result;

        // trigger attribute change event
        OnAttrChange?.Invoke(this, type, _previous, result);
    }

    [SerializeField, Required("MBAttrHolder is missing.")]
    private MBAttrHolder attrHolder;

    [SerializeField, Required("MBCombatInfo is missing")]
    private MBCombatInfo combatInfo;

    // attribute modifier caches
    private AttrModCache[] modCache = InitializeCache();

    /// <summary>
    /// Return an intialized AttrModCache[].
    /// </summary>
    /// <returns></returns>
    private static AttrModCache[] InitializeCache()
    {
        AttrModCache[] _cache = new AttrModCache[EnumArray.AttrType.Length];
        for (int i = 0; i < _cache.Length; ++i)
        {
            _cache[i] = new AttrModCache();
        }
        return _cache;
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
        foreach (AttrModifierTpl _modifier in attrModifier)
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
        for (int i = 0; i < modCache.Length; ++i)
        {
            AttrModCache _attr = modCache[i];
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
        foreach (AttrModCache _charAttr in modCache)
        {
            // reset multiplicative modifiers back to 1
            for (int i = 0; i < _charAttr.Modifiers.Length; ++i)
            {
                _charAttr.Modifiers[i] = 1;
            }

            // reset additive modifiers back to 0
            _charAttr.Modifiers[(int)EAttrModLayer.Additive] = 0;

            _charAttr.independent.Clear();
        }
    }

    private void Awake()
    {
        // get all attributes ready
        RefreshAll(false);
    }

    /// <summary>
    /// Add or remove modifier.
    /// </summary>
    /// <param name="attrModifier"></param>
    /// <param name="isAdd"></param>
    private void ChangeModifiers(AttrModifierTpl attrModifier, bool isAdd)
    {
        AttrModCache _charAttr = modCache[((int)attrModifier.AttrType)];

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
        _charAttr.IsModDirty = true;
    }

    /// <summary>
    /// Recalculate a certain attribute.
    /// </summary>
    /// <param name="type"></param>
    private void RecalculateAttr(EAttrType type)
    {
        AttrModCache _modCache = modCache[(int)type];
        float _additive = _modCache.Modifiers[(int)EAttrModLayer.Additive];
        float _multiplicative = 1;
        float _previous = attrHolder.GetAttr(type);

        // calculate final multiplicative modification
        for (int i = 0; i < _modCache.Modifiers.Length; ++i)
        {
            if (i != (int)EAttrModLayer.Additive)
            {
                _multiplicative *= _modCache.Modifiers[i];
            }
        }

        // reset dirty flag
        _modCache.IsModDirty = false;

        float result = (attrHolder.PresetAttr.GetBase(type) + _additive) * _multiplicative;

        attrHolder.SetAttr(type, result);

        if (type == EAttrType.Vitality)
        {
            combatInfo.MaxHealth = attrHolder.GetAttr(type) * attrHolder.PresetAttr.VitalityMultiple;
        }
        else if (type == EAttrType.Spirit)
        {
            combatInfo.MaxMana = attrHolder.GetAttr(type) * attrHolder.PresetAttr.SpiritMultiple;
        }
    }
}

