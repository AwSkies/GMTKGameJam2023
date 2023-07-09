using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Entity entity;
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float maxTime;

    public Vector2 direction;
    protected float time;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.AngleAxis(-Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (entity == null)
        {
            entity = GetComponent<Entity>();
        }
        if (Vector3.Distance(transform.position, entity.player.transform.position) > maxDistance || time > maxTime)
        {
            Destroy(gameObject);
        }
    }

    protected void FixedUpdate()
    {
        time += Time.deltaTime;
        rigidBody.velocity = direction * speed + Physics2D.gravity * time * rigidBody.gravityScale;
        transform.rotation = Quaternion.AngleAxis(-Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90, Vector3.forward);
    }
}
