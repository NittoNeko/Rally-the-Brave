using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Take and store statuses.
/// </summary>
public interface IStatusTaker
{

    /// <summary>
    /// Apply a single status. Can be resisted. 
    /// Return true if applie, otherwise false.
    /// MBAttrHolder can be null if no attribute-linked status.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    bool TakeStatus(IStatus status, int stack = 1, float timePercent = 1);

    /// <summary>
    /// Remove a status based on its source id. Ignoring all conditions if isForce.
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="isForce"></param>
    void RemoveStatus(int sourceId, bool isForce = false);

    /// <summary>
    /// Remove certain stacks of a statusbased on its source id. Ignoring all conditions if isForce.
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="stack"></param>
    /// <param name="isForce"></param>
    void RemoveStatus(int sourceId, int stack, bool isForce = false);

    /// <summary>
    /// True if a special status exists, otherwise false.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool ExistSpecialStatus(ESpecialEffectType type);
}
