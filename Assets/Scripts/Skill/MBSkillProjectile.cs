using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile typically disappear after several hits.
/// </summary>
public class MBSkillProjectile : MonoBehaviour
{
    [SerializeField]
    private SOSkillProjectileTpl source;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
