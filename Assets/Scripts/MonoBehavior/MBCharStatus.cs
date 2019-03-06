using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// Take statuses and apply their effects.
/// </summary>
public class MBCharStatus : SerializedMonoBehaviour, IStatusTaker, IAttrModifier
{
    public MBCharCont character;

    [Required("This component needs at least one IAttrModifiables to apply modifiers.")]
    public IAttrModifiable attrModifiable;

    /// <summary>
    /// <see cref="IStatusTaker.TakeStatus(SOCharStatus, byte, float)"/>
    /// </summary>
    /// <param name="content"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    public void TakeStatus(SOCharStatus content, byte stack, float timePercent)
    {
        // Try to stack status first
        // True if successfully stacked, otherwise create new status container
        if (!TryStack(content, stack, timePercent))
        {
            CharStatus _status = new CharStatus(stack, content.InitialTime * timePercent, content);
            character.Status.Add(_status);
            ApplyModifier(_status.Content.AttrModifiers, _status.Stack);
        }
    }
    
    /// <summary>
    /// <see cref="IAttrModifier.ApplyAllModifiers"/>
    /// </summary>
    public void ApplyAllModifiers()
    {
        foreach(CharStatus _charStatus in character.Status)
        {
            ApplyModifier(_charStatus.Content.AttrModifiers, _charStatus.Stack, false);
        }

        attrModifiable.RefreshAll(true);
    }

    private void Awake()
    {
    }

    private void Update()
    {

    }

    /// <summary>
    /// True if same status found and stacked, otherwise false.
    /// </summary>
    /// <returns></returns>
    private bool TryStack(SOCharStatus content, byte stack, float timePercent)
    {
        // Find if this status has already existed and stack them
        foreach (CharStatus _status in character.Status)
        {
            if (_status.Content == content)
            {
                byte _prevStack = _status.Stack;
                _status.RemainTime += content.InitialTime * timePercent;
                _status.Stack += stack;
                ApplyModifier(_status.Content.AttrModifiers, (byte)(_status.Stack - _prevStack));
                return true;
            }
        }

        // No such status exists
        return false;
    }

    /// <summary>
    /// Applies certain stacks of modifiers with/out refreshing attirbutes.
    /// </summary>
    /// <param name="modifiers"></param>
    /// <param name="stackDelta"></param>
    /// <param name="isRefresh"></param>
    private void ApplyModifier(AttrModifier[] modifiers, byte stackDelta, bool isRefresh = true)
    {
        // Apply the same modifiers for stackDelta times
        for(int i = 0; i < stackDelta; ++i)
        {
            attrModifiable.TakeModifier(modifiers, false);
        }
    }

    private void UpdateStatus()
    {
        // Reduce the remaining time of statuses
        foreach(CharStatus _status in character.Status)
        {

        }
    }
}
