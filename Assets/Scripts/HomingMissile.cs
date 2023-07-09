using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile
{
    private Entity entity;

    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject.GetComponent<Entity>();
    }

    new void FixedUpdate()
    {
        direction = entity.player.GetComponent<KaijuController>().GetDirectionToFrom(transform.position);
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
