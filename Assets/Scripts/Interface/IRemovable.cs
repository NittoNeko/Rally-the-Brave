using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RemoveHandler(Transform transform);

public interface IRemovable
{
    event RemoveHandler OnRemove;

    // Manually remove a gameobject from the world
    // For player it is a game over
    // Trigger OnRemove to inform others
    void Remove();
}
