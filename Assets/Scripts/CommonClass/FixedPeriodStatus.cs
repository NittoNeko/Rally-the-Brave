using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPeriodStatus
{
    private FixedPeriodStatusTpl source;

    private float counter;

    public float Counter
    {
        get => counter;
        set
        {
            counter = Mathf.Min(Mathf.Max(0, value), Source.Interval);
        }
    }

    public FixedPeriodStatusTpl Source => source;

    public FixedPeriodStatus(FixedPeriodStatusTpl source)
    {
        this.source = source;
    }
}
