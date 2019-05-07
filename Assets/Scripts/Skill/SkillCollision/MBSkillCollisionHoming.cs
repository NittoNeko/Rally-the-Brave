using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MBSkillCollisionHoming : MonoBehaviour, ISkillCollisionCombatInfo
{
    [SerializeField, MinValue(0), Tooltip("0 means it should not be rotated.")]
    private float angularSpeed;
    [SerializeField]
    private float searchRadius;

    private Rigidbody2D rigid;

    private ECombaterFaction casterFaction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Reporter.ComponentMissingCheck(rigid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, searchRadius);
    }

    public void SetCombatInfo(ECombaterFaction casterFaction, IAttrHolder casterAttr)
    {
        this.casterFaction = casterFaction;
    }
}
