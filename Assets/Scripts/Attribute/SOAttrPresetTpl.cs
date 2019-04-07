using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Data container for pre-defined attributes.
/// </summary>
[CreateAssetMenu(fileName = "AttributePreset", menuName = "ScriptableObject/AttributePreset")]
public class SOAttrPresetTpl : ScriptableObject
{
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true), InlineProperty]
    private AttrTplWrapper[] attrPresets;

    public SOAttrPresetTpl()
    {
        attrPresets = Initialize();
    }

    public AttrPresetTpl GetAttrPreset(EAttrType type)
    {
        return attrPresets[(int)type].AttrTpl;
    }

    private AttrTplWrapper[] Initialize()
    {
        AttrTplWrapper[] temp = new AttrTplWrapper[EnumArray.AttrType.Length];

        for (int i = 0; i < temp.Length; ++i)
        {
            temp[i] = new AttrTplWrapper(EnumArray.AttrName[i]);
        }

        return temp;
    }

    private class AttrTplWrapper
    {
        [SerializeField, HideInInspector]
        private string name;
        [SerializeField, InlineProperty]
        private AttrPresetTpl attrTpl;

        public AttrTplWrapper(string name)
        {
            this.name = name;
        }

        public AttrPresetTpl AttrTpl { get => attrTpl; }
    }
}