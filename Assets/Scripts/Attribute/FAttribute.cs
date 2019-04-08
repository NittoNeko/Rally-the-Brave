using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FAttribute
{
    public static IAttribute Create(AttrPresetTpl attrPreset, EAttrType type)
    {
        bool _isInversed = false;
        // decide type of calculator
        switch (type)
        {
            case EAttrType.Dodge:
            case EAttrType.Armor:
                _isInversed = true;
                break;
        }

        return new Attribute(attrPreset, _isInversed);
    }
}