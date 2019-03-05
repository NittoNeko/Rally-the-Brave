using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttr
{
    private static readonly byte indSize = 2;
    private bool isModDirty = true;
    private EAttrType type;
    private float value;
    private readonly float[] modifiers = new float[EnumArray.AttrModLayer.Length]; // Caches of modifiers
    private readonly List<float> independent = new List<float>(indSize); // To avoid imprecision, keep track of independent multiplicative

    public float Value { get => value; set => this.value = value; }

    public float[] Modifiers => modifiers;

    public List<float> Independent => independent;

    public bool IsModDirty { get => isModDirty; set => isModDirty = value; }
    public EAttrType Type { get => type; }

    public void Reset()
    {
        value = 0;
        for(int i = 0; i < modifiers.Length; ++i)
        {
            modifiers[i] = 0;
        }

        independent.Clear();
    }

    public CharAttr(EAttrType type)
    {
        this.type = type;
    }
}
