using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBPlayerDespawnMgr : MonoBehaviour, IDespawnable
{
    // prevent multiple calling of despawn
    private bool isDespawning = false;

    public void Despawn()
    {
        // break out if is despawning
        if (isDespawning) return;

        // close the door for self
        isDespawning = true;
    }
}
