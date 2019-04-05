using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FAttribute
{
    public static IAttribute Create(AttrBoundaryTpl attrPreset, EAttrType type)
    {
        return new Attribute(attrPreset, type);
    }
}