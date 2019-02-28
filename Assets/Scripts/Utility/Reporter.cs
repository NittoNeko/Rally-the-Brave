using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Reporter
{
    [System.Diagnostics.Conditional("DEBUG")]
    public static void AttrMissing(EAttrType type)
    {
        Debug.LogError("EAttrType " + type + " is missing. It may be caused by an unhandled type.");
    }


}
