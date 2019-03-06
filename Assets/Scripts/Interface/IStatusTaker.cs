using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Take and store statuses.
/// </summary>
public interface IStatusTaker
{
    /// <summary>
    /// Apply a single status with certain stacks and time.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    void TakeStatus(SOCharStatus content, byte stack, float timePercent);
}
