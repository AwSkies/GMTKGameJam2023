using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField]
    protected Transform[] directions;
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected float frequency;
    [SerializeField]
    protected ProjectileFirer projectileFirer;

    protected float time;

    // Start is called before the first frame update
    protected void Start()
    {

    }

    protected void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= frequency)
        {
            time -= frequency;
            Fire();
        }
    }

    protected virtual void Fire()
    {
        foreach (Transform fireDirection in directions)
        {
            Debug.Log(fireDirection.ToString());
            projectileFirer.FireInDirection(projectile, fireDirection.localPosition);
        }
    }
}
