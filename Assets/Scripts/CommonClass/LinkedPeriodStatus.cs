using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPeriodStatus
{
    private LinkedPeriodStatusTpl source;

    // power is the snapshot of percent * attribute at the moment it is applied
    private float power;

    private float counter;

    public float Counter
    {
        get => counter;
        set
        {
            counter = Mathf.Min(Mathf.Max(0, value), source.Interval);
        }
    }

    public float Power { get => power; set => power = value; }
    public LinkedPeriodStatusTpl Source => source;

    public LinkedPeriodStatus(LinkedPeriodStatusTpl source)
    {
        this.source = source;
    }
}
