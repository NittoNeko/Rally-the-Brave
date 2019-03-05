using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class MBCharStatus : SerializedMonoBehaviour, IStatusTaker, IAttrModifier
{
    private byte[] SpecialStatus = new byte[EnumArray.SpecialStatusType.Length];

    private List<CharStatus> statuses = new List<CharStatus>(10);

    // Listen on any modifiables in order to send attributes modifiers
    [Required("This component needs at least one IAttrModifiables to send them modifiers.")]
    public IAttrModifiable attrModifiable;

    public void TakeStatus(SOCharStatus content, byte stack, float timePercent)
    {
        // Try to stack status first
        // True if successfully stacked, otherwise create new status container
        if (!TryStack(content, stack, timePercent))
        {
            CharStatus _status = new CharStatus(stack, content.InitialTime * timePercent, content);
            statuses.Add(_status);
            ApplyModifier(_status.Content.AttrModifiers, _status.Stack);
        }
    }

    
    /// <summary>
    /// Apply all modifiers without clearing or refreshing.
    /// </summary>
    public void ApplyAllModifiers()
    {
        foreach(CharStatus _charStatus in statuses)
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
        foreach (CharStatus _status in statuses)
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


    private void ApplyModifier(AttrModifier[] modifiers, byte stackDelta, bool isRefresh = true)
    {
        // For every changed attributes, recalculate them
        for(int i = 0; i < stackDelta; ++i)
        {
            attrModifiable.TakeModifier(modifiers[i])
        }
    }

    private void UpdateStatus()
    {
        // Reduce the remaining time of statuses
        foreach(CharStatus _status in statuses)
        {

        }
    }
}
