using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store all active statuses on this game entity.
/// </summary>
public class MBStatusHolder : MonoBehaviour
{
    // special status caches to quickly know if a special status exists
    private int[] specialStatus = new int[EnumArray.SpecialStatusType.Length];

    // lists of statuses that exist on this entity
    private List<Status> status = new List<Status>(10);

    public int[] SpecialStatus => specialStatus;

    public List<Status> Status => status;
}
