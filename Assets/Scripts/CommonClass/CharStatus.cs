using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data collection of character status.
/// </summary>
public class CharStatus
{
    private byte stack;
    private float remainTime;
    private readonly SOCharStatus content;

    public byte Stack
    {
        get => stack;
        set
        {
            stack = (byte)Mathf.Max(Mathf.Min(stack, content.MaxStack), 0);
        }
    }
    public float RemainTime
    {
        get => remainTime;
        set
        {
            remainTime = (byte)Mathf.Max(Mathf.Min(remainTime, content.MaxTime), 0);
        }
    }
    public SOCharStatus Content { get => content; }

    public CharStatus(byte stack, float remainTime, SOCharStatus content)
    {
        this.Stack = stack;
        this.RemainTime = remainTime;
        this.content = content;
    }
}