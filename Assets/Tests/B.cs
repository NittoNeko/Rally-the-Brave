using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "B", menuName = "ScriptableObject/B")]
public class B : SerializedScriptableObject
{
    [SerializeField]
    private A a;
}

[System.Serializable]
public class A
{
    [Sirenix.Serialization.OdinSerialize]
    private HashSet<int> C;
}
