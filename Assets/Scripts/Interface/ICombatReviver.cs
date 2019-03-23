using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatReviver
{
    /// <summary>
    /// Return true if revive successfully, otherwise false.
    /// </summary>
    /// <returns></returns>
    bool TryRevive();
}
