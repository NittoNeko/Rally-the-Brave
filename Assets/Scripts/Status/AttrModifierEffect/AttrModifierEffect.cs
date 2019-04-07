using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrModifierEffect : IAttrModifierEffect
{
    private ActualModifier[] modifiers;
    private IAttrModifierTaker taker;
    private int stack;

    public AttrModifierEffect(AttrModifierTpl[] modifiers, IAttrModifierTaker taker)
    {
        for ( int i = 0; i < modifiers.Length; ++i)
        {
            this.modifiers[i] = new ActualModifier(modifiers[i]);
        }
        
        this.taker = taker;
        this.stack = 0;
    }

    public void OnStackChange(int stack)
    {
        int _delta = stack - this.stack;

        // add modifier if positive
        if (_delta > 0)
        {
            ApplyAll(_delta);
        } else if (_delta < 0)
        {
            RemoveAll(Mathf.Abs(_delta));
        }
    }

    private void ApplyAll(int stack)
    {
        for (int i = 0; i < stack; ++i)
        {
            for (int j = 0; j < modifiers.Length; ++j)
            {
                taker.TakeModifier(modifiers[j].Actual, modifiers[j].AttrModifier.AttrType, modifiers[j].AttrModifier.Layer, false);
            }
        }

        // refresh
        taker.RefreshAll();
    }

    private void RemoveAll(int stack)
    {
        for (int i = 0; i < stack; ++i)
        {
            for (int j = 0; j < modifiers.Length; ++j)
            {
                taker.RemoveModifier(modifiers[j].Actual, modifiers[j].AttrModifier.AttrType, modifiers[j].AttrModifier.Layer, false);
            }
        }

        // refresh
        taker.RefreshAll();
    }

    private class ActualModifier
    {
        private float actual;
        private AttrModifierTpl attrModifier;

        public AttrModifierTpl AttrModifier { get => attrModifier; }
        public float Actual { get => actual; }

        public ActualModifier(AttrModifierTpl attrModifier)
        {
            this.attrModifier = attrModifier;
        }

        // reroll actual values
        public void Reroll()
        {
            actual = Random.Range(attrModifier.MinValue, attrModifier.MaxValue);
        }
    }
}
