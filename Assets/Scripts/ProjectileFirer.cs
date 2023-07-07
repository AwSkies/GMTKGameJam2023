using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFirer : MonoBehaviour
{
    public GameObject Fire(GameObject projectilePrefab, Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = direction.normalized;
        return projectile;
    }

    public GameObject FireAt(GameObject projectilePrefab, Vector2 direction)
    {
        return Fire(projectilePrefab, (direction - new Vector2(transform.position.x, transform.position.y)).normalized);
    }
}
