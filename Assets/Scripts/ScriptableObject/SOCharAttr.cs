using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


// SOCharAttr is meant to work with MBCharAttr and GenericAttrSet
// This is a data container for pre-defined attributes
[CreateAssetMenu(fileName = "CharAttr", menuName = "ScriptableObject/CharAttr")]
public class SOCharAttr : ScriptableObject
{
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true)]
    private readonly Wrapper[] attr = Initialize();

    public float Get(EAttrType type)
    {
        return attr[(int)type].Value;
    }

    private static Wrapper[] Initialize()
    {
        Wrapper[] temp = new Wrapper[EnumArray.AttrType.Length];

        for (int i = 0; i < temp.Length; ++i)
        {
            temp[i] = new Wrapper(EnumArray.AttrName[i]);
        }

        return temp;
    }

    [System.Serializable]
    private struct Wrapper
    {
        private readonly string name;
        [SerializeField]
        private readonly float value;

        public float Value { get => value;}

        public Wrapper(string name) : this()
        {
            this.name = name;
        }
    }
}