using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MBPlayerController : MonoBehaviour
{
    private IMovable movable;



    private void Awake()
    {
        movable = GetComponent<IMovable>();
        Reporter.ComponentMissingCheck(movable);
    }

    void Update()
    {
        movable.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }


}
