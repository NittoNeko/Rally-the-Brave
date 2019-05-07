using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumArray
{
    public static EAttrType[] AttrType { get; }
        = System.Enum.GetValues(typeof(EAttrType)) as EAttrType[];

    public static string[] AttrTypeName { get; }
        = System.Enum.GetNames(typeof(EAttrType));

    public static EAttrModifierLayer[] AttrModifierLayer { get; }
        = System.Enum.GetValues(typeof(EAttrModifierLayer)) as EAttrModifierLayer[];

    public static ESpecialStatusType[] SpecialStatusType { get; }
    = System.Enum.GetValues(typeof(ESpecialStatusType)) as ESpecialStatusType[];
}
