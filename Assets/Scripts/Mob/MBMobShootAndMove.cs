using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBMobShootAndMove : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float range;
    [SerializeField]
    private CircleCollider2D serachArea;
    private IFactionHolder self;
    private PolyNav.PolyNavAgent agent;
    private float counter;
    private Transform target;
    private bool isAttacking;

    private void Awake()
    {
        self = GetComponent<IFactionHolder>();
        agent = GetComponent<PolyNav.PolyNavAgent>();

        Reporter.ComponentMissingCheck(self);
        Reporter.ComponentMissingCheck(agent);

        serachArea.radius = range;

        isAttacking = false;
    }

    private void Update()
    {
        counter = Mathf.Min(counter + Time.deltaTime, cooldown);

        if (target != null)
        {

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, target.position - transform.position, range);
            if (hit.transform == target)
            {
                isAttacking = true;
                agent.Stop();
                if (counter >= cooldown)
                {
                    Instantiate(bullet).GetComponent<MBProjectileMgr>().Initialize(transform.position, target.position, self.Faction);
                    counter = 0;
                }
            } else
            {
                isAttacking = false;
            }
        } else
        {
            isAttacking = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // in attack mode no following
        if (isAttacking) return;

        IFactionHolder other = collision.GetComponent<IFactionHolder>();

        if (other != null)
        {
            if (other.Faction != self.Faction)
            {
                agent.SetDestination(collision.transform.position);
                target = collision.transform;
            }
        }

    }
}
