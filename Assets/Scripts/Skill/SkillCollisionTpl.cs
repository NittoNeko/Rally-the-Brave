using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillCollisionTpl
{
    [SerializeField]
    private GameObject Collision;
    [SerializeField]
    private SOStatusTpl[] collisionStatus;
    [SerializeField]
    private AttrLinkTpl[] damage;
    [SerializeField]
    private AttrLinkTpl[] healing;
}
