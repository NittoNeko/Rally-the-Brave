using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MBSkillCollisionDirectMovement : MonoBehaviour, ISkillCollisionPointer
{
    [InfoBox("This component assumes the sprite is heading upward.")]
    // 0 means it will never move
    [SerializeField, MinValue(0)]
    private float speed;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Reporter.ComponentMissingCheck(rigid);
    }

    public void SetPosition(Vector2 caster, Vector2 target)
    {
        transform.position = caster;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, target - caster);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = speed * Time.deltaTime * transform.up;
    }


}