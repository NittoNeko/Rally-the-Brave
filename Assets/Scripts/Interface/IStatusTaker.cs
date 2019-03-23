using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatusEvent(SOStatusTpl source);

/// <summary>
/// Take and store statuses.
/// </summary>
public interface IStatusTaker
{
    event StatusEvent OnStatusRemove;

    /// <summary>
    /// Apply a single status. Can be resisted. 
    /// Return true if applie, otherwise false.
    /// MBAttrHolder can be null if no attribute-linked status.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="applierAttr"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    bool TakeStatus(SOStatusTpl source, MBAttrHolder applierAttr, int stack = 1, float timePercent = 1);

    /// <summary>
    /// Remove a status. Ignoring all conditions if isForce.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="isForce"></param>
    void RemoveStatus(SOStatusTpl source, bool isForce = false);

    /// <summary>
    /// Remove certain stacks of a status. Ignoring all conditions if isForce.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="stack"></param>
    /// <param name="isForce"></param>
    void RemoveStatus(SOStatusTpl source, int stack, bool isForce = false);
}
