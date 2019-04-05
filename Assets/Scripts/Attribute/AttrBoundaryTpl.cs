using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This templates defines initial, maximum and minimum value for an attribute with name.
/// </summary>
[System.Serializable]
public class AttrBoundaryTpl
{
    [SerializeField]
    private float initial;
    [SerializeField]
    private float max;
    [SerializeField]
    private float min;

    public float Initial => initial;

    public float Max => max;

    public float Min => min;
}
