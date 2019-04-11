using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MBMoveMgr : MonoBehaviour, IMovable
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    private int SpeedId;
    private int xAxisId;
    private int yAxisId;

    private IAttrHolder attrHolder;

    private void Awake()
    {
        // retrieve rigid body 2d on this game object
        rigidBody = GetComponent<Rigidbody2D>();

        // retrieve animator on this game object
        animator = GetComponent<Animator>();

        attrHolder = GetComponent<IAttrHolder>();
        Reporter.ComponentMissingCheck(attrHolder);

        SpeedId = Animator.StringToHash("Speed");
        xAxisId = Animator.StringToHash("X Axis");
        yAxisId = Animator.StringToHash("Y Axis");
    }

    /// <summary>
    /// <see cref="IMovable.Move(float, float)"/>
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public void Move(float horizontal, float vertical)
    {
        // get normalized velocity
        Vector2 _velocity = new Vector2(horizontal, vertical);

        // set velocity to what we want
        rigidBody.velocity = _velocity * attrHolder.GetAttr(EAttrType.MoveSpeed) * Time.deltaTime;

        // change direction only velocity is not 0
        if (_velocity.sqrMagnitude != 0)
        {
            animator.SetFloat(xAxisId, horizontal);
            animator.SetFloat(yAxisId, vertical);
        }

        animator.SetFloat(SpeedId, _velocity.sqrMagnitude);
    }
}
