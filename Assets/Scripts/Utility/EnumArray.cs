using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumArray
{
    public static EAttrType[] AttrType { get; }
        = System.Enum.GetValues(typeof(EAttrType)) as EAttrType[];

    public static string[] AttrName { get; }
        = System.Enum.GetNames(typeof(EAttrType));

    public static EAttrModifierLayer[] AttrModLayer { get; }
        = System.Enum.GetValues(typeof(EAttrModifierLayer)) as EAttrModifierLayer[];

    public static ESpecialEffectType[] SpecialStatusType { get; }
    = System.Enum.GetValues(typeof(ESpecialEffectType)) as ESpecialEffectType[];

}
