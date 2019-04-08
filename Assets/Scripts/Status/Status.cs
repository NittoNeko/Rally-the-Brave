using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Status : IStatus
{
    private int sourceId;
    private StatusTpl statusTpl;
    private int stack;
    private float remainTime;
    private bool isExpired;
    private IStatusTaker statusTaker;
    private IStatusUpdatable[] statusUpdatables;
    private IStatusStackable[] statusStackables;
    private System.Action Updater;

    public event Action OnExpire;

    public int SourceId { get => sourceId; }

    public EStatusType Type { get => statusTpl.Type; }

    public bool IsExpired => isExpired;

    public bool IsRemovable => statusTpl.IsRemovable;

    public float RemainTime
    {
        get => remainTime;
        set
        {
            remainTime = (int)Mathf.Max(Mathf.Min(value, statusTpl.MaxTime), 0);

            // check expired
            if (remainTime == 0)
            {
                SetExpired();
            }
        }
    }

    public int Stack
    {
        get => stack;
        set
        {
            // range check
            stack = Mathf.Max(Mathf.Min(value, statusTpl.MaxStack), 0);

            // delegate downward
            for (int i = 0; i < statusUpdatables.Length; ++i)
            {
                statusStackables[i].Stack = stack;
            }

            // check expired
            if (stack == 0)
            {
                SetExpired();
            }
        }
    }

    public Status(StatusTpl statusTpl, int sourceId, IStatusUpdatable[] statusUpdatables, IStatusStackable[] statusStackables, int stack, float timePercent)
    {
        this.statusTpl = statusTpl;
        this.sourceId = sourceId;
        this.statusUpdatables = statusUpdatables;
        this.statusStackables = statusStackables;
        this.RemainTime = timePercent * statusTpl.InitialTime;

        // leave this the last one as it informs others
        this.Stack = stack;
        this.Updater = UpdateAll;

    }

    /// <summary>
    /// <see cref="IStatus.RemainTime"/>
    /// </summary>
    public void RefreshTime()
    {
        RemainTime += statusTpl.InitialTime;
    }

    /// <summary>
    /// <see cref="IStatus.SetExpired"/>
    /// </summary>
    public void SetExpired()
    {
        // make sure stack is 0 so that effects are gone
        if (Stack != 0)
        {
            Stack -= Stack;
        }

        // set flag true
        isExpired = true;

        // inform anyone intersted in this status expired
        OnExpire();

        // set update to null
        Updater = () => { };
    }

    public void OnUpdate()
    {
        Updater();
    }

    /// <summary>
    /// Delegate frame updates downward.
    /// </summary>
    private void UpdateAll()
    {
        for (int i = 0; i < statusUpdatables.Length; ++i)
        {
            statusUpdatables[i].OnUpdate();
        }
    }
}