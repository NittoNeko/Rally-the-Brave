using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ReviveStatusTpl
{
    [SerializeField, MinValue(0)]
    private float healthPercent;
    [SerializeField, MinValue(0)]
    private float manaPercent;
    [SerializeField]
    private float isActive;

    public float HealthPercent => healthPercent;
    public float ManaPercent => manaPercent;
    public float IsActive => isActive;
}
