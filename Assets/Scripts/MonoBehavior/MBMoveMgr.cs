using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MBMoveMgr : MonoBehaviour, IMovable
{
    [SerializeField, Required("Status Storage Container is missing.")]
    private MBStatusHolder statusStgCtn;

    [SerializeField, Required("Attribute Container is missing.")]
    private MBAttrHolder attrCtn;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontal, float vertical)
    {
        // return if cannot move
        if (statusStgCtn.SpecialStatus[(int)ESpecialStatusType.Snared] > 0) return;

        Vector3 _velocity = new Vector3(horizontal, vertical) * Time.deltaTime * attrCtn.GetAttr(EAttrType.MoveSpeed);

        rigidbody.velocity = _velocity;
    }
    
}
