using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : Aim
{
    private Entity entity;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        entity = GetComponent<Entity>();
    }

    protected override void Fire()
    {
        projectileFirer.FireAt(projectile, entity.player.transform.position);
    }
}
