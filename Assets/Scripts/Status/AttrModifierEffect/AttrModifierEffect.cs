using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrModifierEffect : IAttrModifierEffect
{
    private ActualModifier[] modifiers;
    private IAttrModifiable taker;
    private int stack;

    public AttrModifierEffect(AttrModifierTpl[] modifiers, IAttrModifiable taker)
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
            taker.TakeModifier();
        }
    }

    public void OnApply()
    {
        // apply the same modifiers for stackDelta times
        foreach (ActualModifier _modifier in modifiers)
        {
            for (int i = 0; i < stackDelta; ++i)
            {
                taker.TakeModifier(_modifier, _modifier., false);
            }
        }
    }

    public void OnRemove()
    {
        for (int i = 0; i < stackDelta; ++i)
        {
            taker.RemoveModifier(_modifier, false);
        }
    }



    /// <summary>
    /// Applies certain stacks of modifiers with/out refreshing attirbutes.
    /// </summary>
    /// <param name="modifiers"></param>
    /// <param name="stackDelta"></param>
    /// <param name="isRefresh"></param>
    private void ApplyModifier(AttrModifierTpl[] modifiers, int stackDelta, bool isRefresh = true)
    {
        // apply the same modifiers for stackDelta times
        foreach (AttrModifierTpl _modifier in modifiers)
        {
            for (int i = 0; i < stackDelta; ++i)
            {
                taker.TakeModifier(new SAttrModifier( _modifier, _modifier., false);
            }
        }

        if (isRefresh) taker.RefreshAll(true);
    }


    /// <summary>
    /// Remove certain stacks of modifiers with/out refreshing attributes.
    /// </summary>
    /// <param name="modifiers"></param>
    /// <param name="stackDelta"></param>
    /// <param name="isRefresh"></param>
    private void RemoveModifier(AttrModifierTpl[] modifiers, int stackDelta, bool isRefresh = true)
    {
        // remove the same modifiers for stackDelta times
        foreach (AttrModifierTpl _modifier in modifiers)
        {
            for (int i = 0; i < stackDelta; ++i)
            {
                taker.RemoveModifier(_modifier, false);
            }
        }

        if (isRefresh) taker.RefreshAll(true);
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
