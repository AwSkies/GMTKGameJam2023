using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : Aim
{
    private Entity entity;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private int cardinalPoints;

    private Vector2 direction;
    private float lastAngle = 180;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        entity = gameObject.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new void FixedUpdate()
    {
        direction = SnapToCardinalPoint(entity.player.GetComponent<KaijuController>().GetDirectionToFrom(transform.position));
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
        for (int i = 0; i < cardinalPoints; i++)
        {
            float angle = 2 * Mathf.PI * i / cardinalPoints;
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            if (Vector2.Distance(vector, point) < Vector2.Distance(closest, vector))
            {
                closest = point;
            }
        }
        return closest;
    }
}
