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
    /// Return true if applied, otherwise false.
    /// Should always call TryStackStatus first.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    bool TakeStatus(IStatus status);

    /// <summary>
    /// Remove a status based on its source id. Ignoring all conditions.
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="isForce"></param>
    void RemoveStatus(int sourceId);
}
