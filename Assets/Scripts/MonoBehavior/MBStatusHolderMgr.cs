using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Take statuses and apply their effects.
/// </summary>
/// Most of cases a status is set expired first, and removed in Update.
/// This is to prevent from removing an element of a list while looping through the list.
public class MBStatusHolderMgr : SerializedMonoBehaviour, IStatusTaker, IAttrModifier
{
    public event StatusEvent OnStatusRemove;

    [SerializeField, Required("MBStatusHolder is missing.")]
    private MBStatusHolder statusHolder;

    [SerializeField, Required("IAttrModifiable is missing.")]
    private IAttrModifiable attrModifiable;

    [SerializeField, Required("ICombator is missing.")]
    private ICombater combator;

    // true if any expired status needs to be removed
    private bool isExpireDirty = false;

    /// <summary>
    /// <see cref="IStatusTaker.TakeStatus(SOStatusTpl, MBAttrHolder, int, float)"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    public bool TakeStatus(SOStatusTpl source, MBAttrHolder applierAttr, int stack = 1, float timePercent = 1)
    {
        // dont apply if immune to debuff
        if (source.Type == EStatusType.Debuff && 
            statusHolder.SpecialStatus[(int)ESpecialStatusType.Purified] > 0)
            return false;

        // search for the same status
        int _index = SearchFor(source);
        int _stackDelta;
        Status _status;


        if (_index < 0) // not found
        {
            _status = new Status(source, stack, timePercent);
            _stackDelta = _status.Stack;
            statusHolder.Status.Add(_status);
            // store special status caches only if new status
            IncrementSpecialStatus(_status);
        } else // found
        {

            _status = statusHolder.Status[_index];
            // record previous value
            _stackDelta = _status.Stack;
            _status.RemainTime += source.InitialTime * timePercent;
            _status.Stack += stack;
            // make sure int not overflow
            _stackDelta = _status.Stack > _stackDelta ? (_status.Stack - _stackDelta) : 0;
        }

#if DEBUG
        // 
        if (source.LinkedPeriod.Length != 0 && applierAttr == null)
        {
            Debug.LogError("Applying attribute-linked status without attributes provided.");
        }
#endif

        // overwrite linked period effects
        if (applierAttr != null) OverwritePeriodEffect(_status, applierAttr);

        // apply attribute modifiers
        ApplyModifier(source.AttrModifiers, _stackDelta);

        return true;
    }

    /// <summary>
    /// <see cref="IStatusTaker.RemoveStatus(SOStatusTpl, bool)"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="isForce"></param>
    public void RemoveStatus(SOStatusTpl source, bool isForce = false)
    {
        RemoveStatus(source, source.MaxStack, isForce);
    }

    /// <summary>
    /// <see cref="IStatusTaker.RemoveStatus(SOStatusTpl, int, bool)"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="stack"></param>
    /// <param name="isForce"></param>
    public void RemoveStatus(SOStatusTpl source, int stack, bool isForce = false)
    {
        // skip if not force and not removable
        if (!isForce && !source.IsRemovable) return;

        // look for index of status that is being removed
        int _index = SearchFor(source);

        // found if greater than 0
        if (_index >= 0)
        {
            Status _status = statusHolder.Status[_index];

            if (stack >= _status.Stack)
            {
                // status is fully expired
                SetStatusExpired(_status);
            } else
            {
                // remove only stacks of a status
                RemoveStatusEffect(_status, stack);
            }
        }
    }

    /// <summary>
    /// <see cref="IAttrModifier.ApplyAllModifiers"/>
    /// </summary>
    public void ApplyAllModifiers()
    {
        foreach(Status _charStatus in statusHolder.Status)
        {
            ApplyModifier(_charStatus.Source.AttrModifiers, _charStatus.Stack, false);
        }
    }

    private void Update()
    {
        // remove expired status before updating statuses
        RemoveExpired();

        // update all statuses in applying order
        foreach (Status _status in statusHolder.Status)
        {
            UpdateStatus(_status);
        }

    }

    /// <summary>
    /// Return the index of a certain status, otherwise return -1.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private int SearchFor(SOStatusTpl source)
    {
        // search for content
        for (int i = 0; i < statusHolder.Status.Count; ++i)
        {
            if (statusHolder.Status[i].Source == source)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Set a status to expired.
    /// </summary>
    /// <param name="status"></param>
    private void SetStatusExpired(Status status)
    {
        // do nothing if already expired
        if (status.IsExpired) return;
        // set expired
        status.IsExpired = true;
        // tell its time to remove some status
        isExpireDirty = true;
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

    /// <summary>
    /// Applies certain stacks of modifiers with/out refreshing attirbutes.
    /// </summary>
    /// <param name="modifiers"></param>
    /// <param name="stackDelta"></param>
    /// <param name="isRefresh"></param>
    private void ApplyModifier(AttrModifierTpl[] modifiers, int stackDelta, bool isRefresh = true)
    {
        // apply the same modifiers for stackDelta times
        foreach(AttrModifierTpl _modifier in modifiers)
        {
            for (int i = 0; i < stackDelta; ++i)
            {
                attrModifiable.TakeModifier(_modifier, false);
            }
        }

        if (isRefresh) attrModifiable.RefreshAll(true);
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
                attrModifiable.RemoveModifier(_modifier, false);
            }
        }

        if (isRefresh) attrModifiable.RefreshAll(true);
    }

    /// <summary>
    /// Increment special status caches.
    /// </summary>
    private void IncrementSpecialStatus(Status charStatus)
    {
        foreach (ESpecialStatusType _special in charStatus.Source.SpecialStatus)
        {
            statusHolder.SpecialStatus[(int)_special] += 1;
        }
    }

    /// <summary>
    /// Decrement special status caches.
    /// </summary>
    private void DecrementSpecialStatus(Status charStatus)
    {
        foreach (ESpecialStatusType _special in charStatus.Source.SpecialStatus)
        {
            statusHolder.SpecialStatus[(int)_special] -= 1;
        }
    }

    /// <summary>
    /// Overwrite the power of linked period status with higher side.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="attr"></param>
    private void OverwritePeriodEffect(Status original, MBAttrHolder attr)
    {
        foreach (LinkedPeriodStatus _periodStatus in original.LinkedPeriod)
        {
            float _newPower = _periodStatus.Source.Percent * attr.GetAttr(_periodStatus.Source.LinkedType);
            _periodStatus.Power = Mathf.Max(_periodStatus.Power, _newPower);
        }
    }

    /// <summary>
    /// Remove all statuses that are expired.
    /// </summary>
    private void RemoveExpired()
    {
        // return if nothing changed
        if (!isExpireDirty) return;

        // do reverse removing
        for (int i = statusHolder.Status.Count - 1; i >= 0; --i)
        {
            Status status = statusHolder.Status[i];
            // remove its effect
            RemoveStatusEffect(status, status.Source.MaxStack);
            // remove it from list
            statusHolder.Status.RemoveAt(i);
        }

        // reset flag
        isExpireDirty = false;
    }

    /// <summary>
    /// Update a status.
    /// </summary>
    private void UpdateStatus(Status status)
    {
        // update all fixed period effects
        foreach (FixedPeriodStatus _periodStatus in status.FixedPeriod)
        {
            _periodStatus.Counter += Time.deltaTime;

            // interval reached
            if (_periodStatus.Counter >= _periodStatus.Source.Interval)
            {
                // reset counter
                _periodStatus.Counter = 0;
                TriggerPeriodEffect(_periodStatus.Source.Power, _periodStatus.Source.IsPercent, _periodStatus.Source.Type);
            }
        }

        // update all linked period effects
        foreach (LinkedPeriodStatus _periodStatus in status.LinkedPeriod)
        {
            _periodStatus.Counter += Time.deltaTime;

            // interval reached
            if (_periodStatus.Counter >= _periodStatus.Source.Interval)
            {
                // reset counter
                _periodStatus.Counter = 0;
                TriggerPeriodEffect(_periodStatus.Power, false, _periodStatus.Source.Type);
            }
        }

        // reduce remaining time if can expire
        if (status.Source.CanExpire)
        {
            status.RemainTime -= Time.deltaTime;
            // set it to expired if 0 remaining time
            if (status.RemainTime <= 0)
            {
                SetStatusExpired(status);
            }
        }
    }

    /// <summary>
    /// Helper function to update periods effects.
    /// </summary>
    private void TriggerPeriodEffect(float power, bool isPercent, EPeriodStatusType type)
    {
        switch (type)
        {
            case EPeriodStatusType.HealthGain:
                combator.ModifyHealth(power, isPercent);
                break;
            case EPeriodStatusType.HealthLose:
                combator.ModifyHealth(-power, isPercent);
                break;
            case EPeriodStatusType.Damage:
                combator.TakeDamage(power, isPercent);
                break;
            case EPeriodStatusType.Heal:
                combator.TakeHeal(power, isPercent);
                break;
            case EPeriodStatusType.ManaGain:
                combator.ModifyMana(power, isPercent);
                break;
            case EPeriodStatusType.ManaLose:
                combator.ModifyMana(-power, isPercent);
                break;
        }
    }
}