using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float maxDistance;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.gravityScale = gravity;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cam.transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }
}
