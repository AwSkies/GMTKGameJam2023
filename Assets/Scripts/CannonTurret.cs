using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : Aim
{
    [SerializeField]
    private Entity entity;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private Vector2[] cardinalPoints;

    private Vector2 direction;
    private float lastAngle = 180;

    // Update is called once per frame
    void Update()
    {
        
    }

    new void FixedUpdate()
    {
        direction = SnapToCardinalPoint((entity.player.transform.position - transform.position).normalized);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        foreach (Transform fireSource in projectileFirer.fireSources)
        {
            fireSource.RotateAround(pivot.position, Vector3.forward, angle - lastAngle);
            fireSource.rotation = Quaternion.Euler(0, 0, angle);
        }
        lastAngle = angle;
        base.FixedUpdate();
    }

    protected override void Fire()
    {
        projectileFirer.FireInDirection(projectile, direction);
    }

    private Vector2 SnapToCardinalPoint(Vector2 vector)
    {
        Vector2 closest = Vector2.zero;
        foreach (Vector2 point in cardinalPoints)
        {
            if (Vector2.Distance(vector, point) < Vector2.Distance(closest, vector))
            {
                closest = point;
            }
        }
        return closest;
    }
}
