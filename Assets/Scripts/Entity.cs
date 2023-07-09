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
    private float invulnerabilityTime;
    [SerializeField]
    private Animator damageAnimator;
    [SerializeField]
    private GameObject deathParticleEmitter;
    public GameObject player;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hurtSound;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<KaijuController>().gameObject;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        TakeDamage(collision.gameObject.GetComponent<Entity>());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        OnTriggerStay2D(collision.collider);
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
                    audioSource.PlayOneShot(hurtSound);
                }
                catch (UnassignedReferenceException)
                {

                }
                invulnerable = true;
                Invoke("MakeVulnerable", invulnerabilityTime);
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die() 
    {
        Instantiate(deathParticleEmitter, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void MakeVulnerable()
    {
        invulnerable = false;
    }
}
