using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Reporter
{
    /// <summary>
    /// Check whether components are missing on the same gameobjects.
    /// </summary>
    [System.Diagnostics.Conditional("DEBUG")]
    public static void ComponentMissingCheck(object o)
    {
        if (o == null)
        {
            Debug.LogError("Component " + o.GetType() + " is missing on this gameobject.");
        }
    }

}
