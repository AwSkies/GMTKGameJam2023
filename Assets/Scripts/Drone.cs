using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidBody;
    private Entity entity;
    [SerializeField]
    private Transform fireSource;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private bool moving;

    private Vector2 movementDirection = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (moving)
        {
            movementDirection = entity.player.GetComponent<KaijuController>().GetDirectionToFrom(transform.position);
            rigidBody.velocity = movementDirection * movementSpeed;
        }
        animator.SetBool("Moving", moving);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Horizontal", movementDirection.x);    
        fireSource.localPosition = movementDirection;
    }
}
