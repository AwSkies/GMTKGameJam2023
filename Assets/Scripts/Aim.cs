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
    [SerializeField]
    protected float telegraphFraction;

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
        if (time / frequency >= telegraphFraction)
        {
            foreach (Transform fireSource in projectileFirer.fireSources)
            {
                ParticleSystem particleSystem = fireSource.gameObject.GetComponent<ParticleSystem>();
                if (particleSystem.isStopped)
                {
                    particleSystem.Play();
                }
            }
        }
        else
        {
            foreach (Transform fireSource in projectileFirer.fireSources)
            {
                ParticleSystem particleSystem = fireSource.gameObject.GetComponent<ParticleSystem>();
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
    }

    protected virtual void Fire()
    {
        foreach (Transform fireDirection in directions)
        {
            projectileFirer.FireInDirection(projectile, fireDirection.localPosition);
        }
    }
}
