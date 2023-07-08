using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    private int currentHealth;
    [SerializeField]
    public int damage;
    [SerializeField]
    public bool invulnerable;
    [SerializeField]
    private Animator damageAnimator;
    [SerializeField]
    private ParticleSystem deathParticles;
    [SerializeField]
    private int deathParticlesNumber;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("This object: " + gameObject.name + " Other object: " + collision.gameObject.name + " Method: OnTriggerEnter2D");
        if (gameObject.tag != collision.gameObject.tag)
        {
            TakeDamage(collision.gameObject.GetComponent<Entity>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("This object: " + gameObject.name + " Other object: " + collision.gameObject.name + " Method: OnCollisionEnter2D");
        OnTriggerEnter2D(collision.otherCollider);
    }

    private void TakeDamage(Entity entity)
    {
        if (!entity.invulnerable){
            currentHealth -= entity.damage;
            if (entity.damage > 0)
            {
                damageAnimator.Play("Damage");
            }
            if (currentHealth <= 0)
            {
                deathParticles.Emit(deathParticlesNumber);
                Destroy(gameObject);
            }
        }
    }
}
