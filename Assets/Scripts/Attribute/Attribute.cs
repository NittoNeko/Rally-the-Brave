using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attribute that contains its actual value and modifiers.
/// </summary>
public class Attribute : IAttribute
{
    private AttrBoundaryTpl preset;
    private static readonly int indSize = 2;
    private bool isModDirty;
    // caches of modifiers ordered by enum
    private float[] modifiers;
    // keep track of independent multiplicative to avoid imprecision
    private List<float> independents;
    private float actualValue;
    private System.Action Calculate;

    public float ActualValue => actualValue;

    public Attribute(AttrBoundaryTpl preset, EAttrType type)
    {
        isModDirty = true;
        modifiers = new float[EnumArray.AttrModLayer.Length];
        independents = new List<float>(indSize);
        this.preset = preset;

        switch (type)
        {
            case EAttrType.Vitality:
            case EAttrType.Spirit:
            case EAttrType.Attack:
            case EAttrType.CritRate:
            case EAttrType.MoveSpeed:
            case EAttrType.CritMult:
                Calculate = NormalCalculate;
                break;
            case EAttrType.Dodge:
            case EAttrType.Armor:
                Calculate = InverseCalculate;
                break;
        }
    }

    /// <summary>
    /// Add a modifier to this attribute.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="layer"></param>
    /// <param name="attrType"></param>
    public void TakeModifier(EAttrType attrType, EAttrModLayer layer, float value)
    {
        // keep track of independent
        if (layer == (int)EAttrModLayer.Independent)
        {
            independents.Add(value);
        }
        else
        {
            modifiers[(int)layer] += value;
        }

        // set dirty
        isModDirty = true;
    }

    /// <summary>
    /// Remove a modifier from this attribute.
    /// </summary>
    /// <param name="attrType"></param>
    /// <param name="layer"></param>
    /// <param name="value"></param>
    public void RemoveModifier(EAttrType attrType, EAttrModLayer layer, float value)
    {
        // keep track of independent
        if (layer == (int)EAttrModLayer.Independent)
        {

            independents.Remove(value);
        }
        else
        {
            modifiers[(int)layer] -= value;
        }

        // set dirty
        isModDirty = true;
    }

    /// <summary>
    /// Reset this attribute.
    /// </summary>
    public void Reset()
    {
        // reset multiplicative modifiers back to 1
        for (int i = 0; i < modifiers.Length; ++i)
        {
            modifiers[i] = 1;
        }

        // reset additive modifiers back to 0
        modifiers[(int)EAttrModLayer.Additive] = 0;

        // reset list
        independents.Clear();
    }

    /// <summary>
    /// Recalculate the actual value of this attribute.
    /// </summary>
    public void Recalculate(bool isForced)
    {
        // do not recalculate if not forced and not dirty
        if (!isForced && !isModDirty) return;

        float _result = calculator.Recalculate(modifiers, independents, preset.Initial);

        // range check
        _result = Mathf.Max(Mathf.Min(_result, preset.Max), preset.Min);

        // reset dirty flag
        isModDirty = false;
    }

    /// <summary>
    /// Calculate all multiplicative modifiers by directly multiply them.
    /// For example, actual = base * (1 + 0.5) * (1 + 0.1 + 0.2).
    /// </summary>
    private void NormalCalculate()
    {
        // get additive modification
        float _additive = modifiers[(int)EAttrModLayer.Additive];

        // recalculate caches of independent layer
        float _independent = 1;
        for (int i = 0; i < independents.Count; ++i)
        {
            _independent *= independents[i];
        }
        modifiers[(int)EAttrModLayer.Independent] = _independent;

        // calculate multiplicative modification
        float _multiplicative = 1;
        for (int i = 0; i < modifiers.Length; ++i)
        {
            if (i != (int)EAttrModLayer.Additive)
            {
                _multiplicative *= modifiers[i];
            }
        }

        // calculate actual value
        float _result = (preset.Initial + _additive) * _multiplicative;

        // range check
        _result = Mathf.Max(Mathf.Min(_result, preset.Max), preset.Min);

        isModDirty = false;
    }

    /// <summary>
    /// Calculate multiplicative modifiers in (1 - modifier) manner.
    /// For example, actual = (1 - base) * (1 - 0.5) * (1 - 0.1 - 0.2) 
    /// </summary>
    /// <returns></returns>
    private void InverseCalculate()
    {
        // get additive modification
        float _additive = modifiers[(int)EAttrModLayer.Additive];

        // recalculate caches of independent layer
        float _independent = 1;
        for (int i = 0; i < independents.Count; ++i)
        {
            _independent *= Mathf.Max(0, (1 - independents[i]));
        }
        modifiers[(int)EAttrModLayer.Independent] = _independent;

        // calculate multiplicative modification
        float _multiplicative = 1;
        for (int i = 0; i < modifiers.Length; ++i)
        {
            if (i != (int)EAttrModLayer.Additive)
            {
                _multiplicative *= Mathf.Max(0, (1 - modifiers[i]));
            }
        }

        // calculate actual value
        float _result = Mathf.Max(0, (1 - (initial + _additive)) * _multiplicative);

        return 1 - _result;
    }


}
