using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatus : IStatusStackable, IStatusUpdatable
{
    event System.Action OnExpire;

    int SourceId { get; }
    EStatusType Type { get; }
    float RemainTime { get; set; }
    bool IsExpired { get; }
    bool IsRemovable { get; }

    /// <summary>
    /// Set this status ready for removal.
    /// </summary>
    void SetExpired();

    /// <summary>
    /// Refresh the time based on initial time.
    /// </summary>
    void RefreshTime();
}
