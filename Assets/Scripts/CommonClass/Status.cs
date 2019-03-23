using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Status
{
    private SOStatusTpl source;
    private int stack;
    private float remainTime;
    private bool isExpired = false;
    private FixedPeriodStatus[] fixedPeriod;
    private LinkedPeriodStatus[] linkedPeriod;

    public bool IsExpired { get => isExpired; set => isExpired = value; }

    public int Stack
    {
        get => stack;
        set
        {
            stack = (int)Mathf.Max(Mathf.Min(value, source.MaxStack), 0);
        }
    }

    public float RemainTime
    {
        get => remainTime;
        set
        {
            remainTime = (int)Mathf.Max(Mathf.Min(value, source.MaxTime), 0);
        }
    }

    public SOStatusTpl Source => source;

    public FixedPeriodStatus[] FixedPeriod => fixedPeriod;
    public LinkedPeriodStatus[] LinkedPeriod => linkedPeriod; 

    public Status(SOStatusTpl source, int stack = 1, float timePercent = 1)
    {
        this.source = source;
        this.Stack = stack;
        this.RemainTime = source.MaxTime * timePercent;

        this.fixedPeriod = new FixedPeriodStatus[source.FixedPeriod.Length];
        for (int i = 0; i < this.FixedPeriod.Length; ++i)
        {
            this.FixedPeriod[i] = new FixedPeriodStatus(source.FixedPeriod[i]);
        }

        this.linkedPeriod = new LinkedPeriodStatus[source.LinkedPeriod.Length];
        for (int i = 0; i < this.LinkedPeriod.Length; ++i)
        {
            this.LinkedPeriod[i] = new LinkedPeriodStatus(source.LinkedPeriod[i]);
        }


    }
}