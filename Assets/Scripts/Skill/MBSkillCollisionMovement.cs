using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MBSkillCollisionMovement : MonoBehaviour
{
    // 0 means it will never move
    [SerializeField, MinValue(0)]
    private float speed;
    [SerializeField]
    private ESkillMoveType moveType;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
