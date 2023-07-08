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
        if (!gameObject.CompareTag(collision.gameObject.tag))
        {
            TakeDamage(collision.gameObject.GetComponent<Entity>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    protected void TakeDamage(Entity entity)
    {
        if (!invulnerable)
        {
            currentHealth -= entity.damage;
            if (entity.damage > 0)
            {
                try
                {
                    damageAnimator.Play("Damage");
                }
                catch (UnassignedReferenceException)
                {
                    
                }
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    protected void Die() 
    {
        Instantiate(deathParticleEmitter, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
