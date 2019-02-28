using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalSetting", menuName = "ScriptableObject/GlobalSetting")]
public class SOGlobalSetting : ScriptableObject
{
    [SerializeField]
    private readonly float corpseRemain = 10f;

    public float CorpseRemain => corpseRemain;

}
