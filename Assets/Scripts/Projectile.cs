using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Camera cam;
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
        cam = Camera.main;
        transform.localRotation = Quaternion.AngleAxis(-Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cam.transform.position) > maxDistance || time > maxTime)
        {
            Destroy(gameObject);
        }
    }

    protected void FixedUpdate()
    {
        time += Time.deltaTime;
        rigidBody.velocity = direction * speed + Physics2D.gravity * time * rigidBody.gravityScale;
    }
}
