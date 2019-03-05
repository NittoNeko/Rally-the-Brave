using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Reporter
{
    /// <summary>
    /// Check whether components are missing on the same gameobjects.
    /// </summary>
    [System.Diagnostics.Conditional("DEBUG")]
    public static void ComponentMissing(System.Type type)
    {
        Debug.LogError("Component " + type + " is missing on this gameobject.");
    }

}
