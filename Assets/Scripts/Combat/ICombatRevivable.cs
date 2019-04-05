using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatRevivable
{
    /// <summary>
    /// Register a new reviver.
    /// </summary>
    /// <param name="reviver"></param>
    void AddReviver(ICombatReviver reviver);

    /// <summary>
    /// Deregister a reviver.
    /// </summary>
    /// <param name="reviver"></param>
    void RemoveReviver(ICombatReviver reviver);

    /// <summary>
    /// Consume the first reviver that is available.
    /// Return true if any reviver takes effect, otherwise false.
    /// </summary>
    bool ConsumeReviver();
}
