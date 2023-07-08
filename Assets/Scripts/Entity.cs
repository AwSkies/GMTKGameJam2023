using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int damage;
    [SerializeField]
    public bool invulnerable;
    [SerializeField]
    private Animator damageAnimator;
    [SerializeField]
    private GameObject deathParticleEmitter;
    [SerializeField]
    private int deathParticlesNumber;

    private int currentHealth;


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
        Entity otherEntity = collision.gameObject.GetComponent<Entity>();
        Debug.Log("gameObject.tag: " + gameObject.tag + ", collision.gameObject.tag: " + collision.gameObject.tag);
        if (!gameObject.CompareTag(collision.gameObject.tag))
        {
            // Debug.Log(gameObject.name + " was hit by " + collision.gameObject.name + " and is" + (invulnerable ? "" : " not") + " invulnerable");
            TakeDamage(collision.gameObject.GetComponent<Entity>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    protected void TakeDamage(Entity entity)
    {
        Debug.Log(gameObject.name + " is taking damage from " + entity.gameObject.name + " and is" + (invulnerable ? "" : " not") + " invulnerable");
        if (!invulnerable)
        {
            currentHealth -= entity.damage;
            if (entity.damage > 0)
            {
                damageAnimator.Play("Damage");
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    protected void Die() 
    {
        // Instantiate(deathParticleEmitter);
        Destroy(gameObject);
    }
}
