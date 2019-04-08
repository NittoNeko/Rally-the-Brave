using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObject/Status")]
public class SOStatusTpl : ScriptableObject
{
    [SerializeField, HideLabel]
    private StatusTpl statusTpl;

    public StatusTpl StatusTpl { get => statusTpl; }
}


