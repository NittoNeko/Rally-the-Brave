using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBSkillMgr : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Reporter.ComponentMissingCheck(animator);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
