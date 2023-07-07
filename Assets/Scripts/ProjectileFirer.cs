using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFirer : MonoBehaviour
{
    public Projectile Fire(Projectile projectilePrefab, Vector2 source, Vector2 direction)
    {
        return Instantiate(projectilePrefab, source, Quaternion.LookRotation(direction, new Vector2()));
    }
}
