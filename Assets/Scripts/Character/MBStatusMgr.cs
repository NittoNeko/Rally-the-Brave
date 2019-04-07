using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MBStatusMgr : MonoBehaviour, IStatusTaker
{
    // special status caches to quickly know if a special status exists
    private int[] specialStatus = new int[EnumArray.SpecialStatusType.Length];

    // lists of statuses that exist on this entity
    private List<IStatus> status = new List<IStatus>(10);

    public int[] SpecialStatus => specialStatus;

    public List<IStatus> Status => status;

    public event StatusEvent OnStatusRemove;

    [SerializeField, Required("IAttrModifiable is missing.")]
    private IAttrModifierTaker attrModifiable;

    [SerializeField, Required("ICombater is missing.")]
    private ICombater combater;

    // true if any expired status needs to be removed
    private bool isExpireDirty = false;

    /// <summary>
    /// <see cref="IStatusTaker.TakeStatus(IStatus, MBAttrHolder, int, float)"/>
    /// </summary>
    /// <param name="status"></param>
    /// <param name="sourceId"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    public bool TakeStatus(IStatus status, int stack = 1, float timePercent = 1)
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
        }
        else // found
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
    /// <see cref="IStatusTaker.RemoveStatus(int, bool)"/>
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="isForce"></param>
    public void RemoveStatus(int sourceId, bool isForce = false)
    {
        RemoveStatus(source, source.MaxStack, isForce);
    }

    /// <summary>
    /// <see cref="IStatusTaker.RemoveStatus(int, int, bool)"/>
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="stack"></param>
    /// <param name="isForce"></param>
    public void RemoveStatus(int sourceId, int stack, bool isForce = false)
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
            }
            else
            {
                // remove only stacks of a status
                RemoveStatusEffect(_status, stack);
            }
        }
    }

    /// <summary>
    /// Return the index of a certain status, otherwise return -1.
    /// </summary>
    /// <param name="sourceId"></param>
    /// <returns></returns>
    private int SearchFor(int sourceId)
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
}
