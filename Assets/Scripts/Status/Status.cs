using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Status : IStatus
{
    private int sourceId;
    private StatusTpl statusTpl;
    private int stack;
    private float remainTime;
    private bool isExpired;
    private IPeriodicCombatResourceEffect combatPeriodEffect;

    public bool IsExpired => isExpired;

    public int Stack
    {
        get => stack;
        set
        {
            int _previous = stack;
            stack = Mathf.Max(Mathf.Min(value, statusTpl.MaxStack), 0);
            if (_previous != stack)
            {
                OnStackChange();
            }
        }
    }

    public float RemainTime
    {
        get => remainTime;
        set
        {
            remainTime = (int)Mathf.Max(Mathf.Min(value, statusTpl.MaxTime), 0);
        }
    }

    public int SourceId { get => sourceId; }

    public EStatusType Type { get => statusTpl.Type; }

    public void SetExpired()
    {
        isExpired = true;
    }
    
    public void OnStackChange()
    {
        combatPeriodEffect.OnStackChange(stack);
    }

    public void OnApply()
    {

    }

    public void OnUpdate()
    {
        combatPeriodEffect.OnUpdate();
    }

    public void OnRemove()
    {

    }

    /// <summary>
    /// Remove only effects of a status. 
    /// Do not set the status expired.
    /// Do not remove this status from list.
    /// </summary>
    private void RemoveStatusEffect(Status status, int stack)
    {
        // make sure stack is 0 or positive
        int _stack = Mathf.Max(stack, 0);
        // stack before minus
        int _stackDelta = status.Stack;
        // remaining stack
        status.Stack = _stack == 0 ? 0 : status.Stack - _stack;
        // stack changed
        _stackDelta -= status.Stack;
        // remove certain stacks of attribute modifier
        RemoveModifier(status.Source.AttrModifiers, _stackDelta);

        // decrease stack caches if stack actually changed and stack is zero
        if (status.Stack == 0 && _stackDelta > 0)
        {
            // remove status completely if 0 stack
            DecrementSpecialStatus(status);
        }
    }

    public static class Factory
    {
        public static IStatus Create(SOStatusTpl source, 
            IPeriodicCombatResourceEffect combatPeriodEffect,
            int stack = 1, float timePercent = 1)
        {
            Status _status = new Status();
            // use default effect if null
            _status.combatPeriodEffect = combatPeriodEffect;
            _status.statusTpl = source.StatusTpl;
            
            _status.sourceId = source.GetInstanceID();
            _status.stack = stack;
            _status.RemainTime = _status.statusTpl.MaxTime * timePercent;

            return _status;
        }
    }
}