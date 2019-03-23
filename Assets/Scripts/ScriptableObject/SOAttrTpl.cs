using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Data container for pre-defined attributes.
/// </summary>
[CreateAssetMenu(fileName = "PresetAttribute", menuName = "ScriptableObject/PresetAttribute")]
public class SOAttrTpl : ScriptableObject
{
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true), InlineProperty]
    private Wrapper[] baseAttr;

    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true), InlineProperty]
    private Wrapper[] maxAttr;

    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true), InlineProperty]
    private Wrapper[] minAttr;

    // how is vitality converted
    [SerializeField, MinValue(0)]
    private int vitalityMultiple;

    // how is spirit converted
    [SerializeField, MinValue(0)]
    private int spiritMultiple;

    public int VitalityMultiple => vitalityMultiple;
    public int SpiritMultiple => spiritMultiple;

    public SOAttrTpl()
    {
        baseAttr = Initialize();
        maxAttr = Initialize();
        minAttr = Initialize();
        Debug.Log("SOAttrCtn initialized");
    }

    public float GetBase(EAttrType type)
    {
        return baseAttr[(int)type].value;
    }

    public float GetMax(EAttrType type)
    {
        return maxAttr[(int)type].value;
    }

    public float GetMin(EAttrType type)
    {
        return minAttr[(int)type].value;
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
    private class Wrapper
    {
        [HideInInspector]
        public string name;
        public float value;

        public Wrapper(string name)
        {
            this.name = name;
        }
    }
}