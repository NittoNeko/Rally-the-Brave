using UnityEngine;

public class AttrModifierEffect : IStatusStackable
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

    public int Stack { get => stack;
        set
        {
            int _delta = value - this.stack;

            // add modifier if positive
            if (_delta > 0)
            {
                ApplyAll(_delta);
            }
            else if (_delta < 0)
            {
                RemoveAll(Mathf.Abs(_delta));
            }

            this.stack = value;
        }
    }

    /// <summary>
    /// Apply all modifiers for delta stacks.
    /// </summary>
    /// <param name="delta"></param>
    private void ApplyAll(int delta)
    {
        for (int i = 0; i < delta; ++i)
        {
            for (int j = 0; j < modifiers.Length; ++j)
            {
                taker.TakeModifier(modifiers[j].Actual, modifiers[j].AttrModifier.AttrType, modifiers[j].AttrModifier.Layer, false);
            }
        }

        // refresh
        taker.RefreshAll();
    }

    /// <summary>
    /// Remove all modifiers for delta stacks.
    /// </summary>
    /// <param name="delta"></param>
    private void RemoveAll(int delta)
    {
        for (int i = 0; i < delta; ++i)
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
