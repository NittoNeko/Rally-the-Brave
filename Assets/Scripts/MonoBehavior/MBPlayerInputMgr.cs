using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MBPlayerInputMgr : SerializedMonoBehaviour
{
    [SerializeField, Required("IMovable is missing.")]
    private IMovable movable;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movable.Move(horizontal, vertical);
    }
}
