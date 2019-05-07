using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBProjectileMgr : MonoBehaviour, IFactionHolder
{
    private ECombaterFaction faction;
    private Rigidbody2D rigid;
    [SerializeField]
    private float speed = 100;

    public ECombaterFaction Faction => faction;

    public void Initialize(Vector2 origin, Vector2 target, ECombaterFaction faction)
    {
        this.faction = faction;
        transform.position = origin;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, target - origin);
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Reporter.ComponentMissingCheck(rigid);
    }

    private void Update()
    {
        rigid.velocity = speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IFactionHolder holder = collision.transform.GetComponent<IFactionHolder>();

        // destroy self on enemy/neutral or on obstacles
        if (holder == null || holder.Faction != this.faction)
        {
            Destroy(gameObject);
        }
    }
}
