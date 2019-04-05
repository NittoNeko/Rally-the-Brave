using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Data container for pre-defined attributes.
/// </summary>
[CreateAssetMenu(fileName = "AttributePreset", menuName = "ScriptableObject/AttributePreset")]
public class SOAttrTpl : ScriptableObject
{
    [SerializeField, ListDrawerSettings(IsReadOnly = true, ListElementLabelName = "name", Expanded = true), InlineProperty]
    private AttrTplWrapper[] attrPresets;

    public SOAttrTpl()
    {
        attrPresets = Initialize();
    }

    public AttrBoundaryTpl GetAttrPreset(EAttrType type)
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
        private AttrBoundaryTpl attrTpl;

        public AttrTplWrapper(string name)
        {
            this.name = name;
        }

        public AttrBoundaryTpl AttrTpl { get => attrTpl; }
    }
}