using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float gravity;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.gravityScale = gravity;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }
}
