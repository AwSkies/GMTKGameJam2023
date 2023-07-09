using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile
{
    private Entity entity;
    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
