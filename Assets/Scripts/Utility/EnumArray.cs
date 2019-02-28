using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumArray
{
    public static IEnumerable<EAttrType> AttrType { get; }
        = System.Enum.GetValues(typeof(EAttrType)) as EAttrType[];

    public static IEnumerable<EAttrModLayer> AttrModLayer { get; }
        = System.Enum.GetValues(typeof(EAttrModLayer)) as EAttrModLayer[];

    public static int AttrModLayerCount { get; }
        = System.Enum.GetValues(typeof(EAttrModLayer)).Length;
}
