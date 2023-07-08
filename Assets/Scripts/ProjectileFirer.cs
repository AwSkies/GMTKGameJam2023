using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFirer : MonoBehaviour
{
    [SerializeField]
    private Transform fireSource;

    public GameObject FireInDirection(GameObject projectilePrefab, Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, fireSource.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = direction.normalized;
        return projectile;
    }

    public GameObject FireAt(GameObject projectilePrefab, Vector2 direction)
    {
        return FireInDirection(projectilePrefab, (direction - new Vector2(fireSource.position.x, fireSource.position.y)).normalized);
    }
}
