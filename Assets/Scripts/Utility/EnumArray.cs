using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumArray
{
    public static EAttrType[] AttrType { get; }
        = System.Enum.GetValues(typeof(EAttrType)) as EAttrType[];

    public static string[] AttrName { get; }
        = System.Enum.GetNames(typeof(EAttrType));

    public static EAttrModLayer[] AttrModLayer { get; }
        = System.Enum.GetValues(typeof(EAttrModLayer)) as EAttrModLayer[];

    public static ESpecialStatusType[] SpecialStatusType { get; }
    = System.Enum.GetValues(typeof(ESpecialStatusType)) as ESpecialStatusType[];

}
