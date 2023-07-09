using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KaijuController : MonoBehaviour
{
    #region Component references
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private ProjectileFirer projectileFirer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator slashAnimator;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private GameObject slash;
    [SerializeField]
    private CapsuleCollider2D capsuleCollider;
    [SerializeField]
    private Transform fireSource;
    [SerializeField]
    private ParticleSystem mouthParticles;
    [SerializeField]
    private ParticleSystem winParticles;
    #endregion

    #region Dash parameters
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    [SerializeField]
    private float dashCooldown;
    #endregion

    #region Melee parameters
    [SerializeField]
    private float meleeCooldown;
    [SerializeField]
    private float meleeRadius;
    [SerializeField]
    private float meleeRotationRandomizationRange;
    #endregion

    #region Projectile attack parameters
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireDelay;
    [SerializeField]
    private float fireCooldown;
    #endregion

    [SerializeField]
    private float levelTop;
    [SerializeField]
    private float levelBottom;
    [SerializeField]
    private float winY;

    private System.Random rng = new System.Random();
    private Vector2 movementDirection = new Vector2();
    private Vector2 aimDirectionMelee = new Vector2();
    private Vector2 aimDirectionFire = new Vector2();
    private Vector2 actionDirection = new Vector2();
    private Vector2 lastDirection = new Vector2();
    private float currentSpeed;
    private bool dashing;
    private bool meleeing;
    private bool firing;
    private float colliderOffsetX;
    private float fireSourceX;
    [SerializeField]
    private float fireSourceRotationLeft;
    [SerializeField]
    private float fireSourceRotationRight;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        colliderOffsetX = capsuleCollider.offset.x;
        fireSourceX = fireSource.localPosition.x;
        fireSource.rotation = Quaternion.Euler(0, 0, fireSourceRotationLeft);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - levelBottom) / (levelTop - levelBottom));
        // Move character
        rigidBody.velocity = movementDirection * currentSpeed;
        // Set animation variables
        animator.SetBool("Moving", movementDirection.sqrMagnitude != 0 || meleeing || firing);
        animator.SetBool("Dashing", dashing);
        Vector2 lookingDirection = movementDirection.sqrMagnitude == 0 ? lastDirection : movementDirection;
        float horizontal = firing || meleeing ? actionDirection.x : lookingDirection.x;
        float vertical = firing || meleeing ? actionDirection.y : lookingDirection.y;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        int flip;
        if (horizontal != 0)
        {
            flip = (int) -Mathf.Sign(horizontal);
        }
        else
        {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Left"))
            {
                flip = 1;
            }
            else
            {
                flip = -1;
            }
        }
        capsuleCollider.offset = new Vector2(flip * colliderOffsetX, capsuleCollider.offset.y);
        fireSource.localPosition = new Vector3(flip * fireSourceX, fireSource.localPosition.y);
        if (flip == 1)
        {
            fireSource.rotation = Quaternion.Euler(0, 0, fireSourceRotationLeft);
        }
        else
        {
            fireSource.rotation = Quaternion.Euler(0, 0, fireSourceRotationRight);
        }
        if (transform.position.y >= winY && !winParticles.isPlaying)
        {
            winParticles.Play();
            Invoke("LoadNextLevel", winParticles.main.duration);
        }
    }

    #region Input action listeners
    public void OnMove(InputValue value)
    {
        lastDirection = movementDirection;
        movementDirection = value.Get<Vector2>();
    }

    public void OnAim(InputValue value)
    {
        Vector2 aim = value.Get<Vector2>();
        Vector2 aimFire = aim;
        Vector2 aimMelee = aim;
        // Correct if the scheme is KBM
        if (playerInput.currentControlScheme == "KBM")
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(aim);
            aimFire = mousePosition - (Vector2) fireSource.position;
            aimMelee = mousePosition - (Vector2) transform.position;
        }
        if (aimFire != Vector2.zero)
        {
            aimDirectionFire = aimFire.normalized;
        }
        if (aimMelee != Vector2.zero)
        {
            aimDirectionMelee = aimMelee.normalized;
        }
    }

    public void OnDash()
    {
        if (!dashing)
        {
            dashing = true;
            currentSpeed = dashSpeed;
            Invoke("ResetMovementSpeed", dashTime);
            Invoke("ResetDashCooldown", dashCooldown);
        }
    }

    public void OnMelee()
    {
        if (!meleeing)
        {
            meleeing = true;
            actionDirection = aimDirectionMelee;
            slash.transform.position = aimDirectionMelee * meleeRadius + new Vector2(transform.position.x, transform.position.y);
            slash.transform.localRotation = Quaternion.AngleAxis(-Mathf.Atan2(aimDirectionMelee.x, aimDirectionMelee.y) * Mathf.Rad2Deg + (float) rng.NextDouble() * meleeRotationRandomizationRange, Vector3.forward);
            slash.SetActive(true);
            Invoke("ResetMeleeCooldown", meleeCooldown);
        }
    }

    public void OnFire()
    {
        if (!firing)
        {
            firing = true;
            Invoke("Fire", fireDelay);
        }
    }
    #endregion

    #region Methods to reset cooldowns of actions
    private void ResetMovementSpeed()
    {
        currentSpeed = speed;
    }

    private void ResetDashCooldown()
    {
        dashing = false;
        Breathe();
    }

    private void ResetMeleeCooldown()
    {
        meleeing = false;
    }

    private void Fire()
    {
        actionDirection = aimDirectionFire;
        projectileFirer.FireInDirection(projectilePrefab, aimDirectionFire);
        Invoke("ResetFireCooldown", fireCooldown);
    }
    
    private void ResetFireCooldown()
    {
        firing = false;
        Breathe();
    }
    #endregion

    private void Breathe()
    {
        mouthParticles.Emit(30);
    }

    public Vector2 GetDirectionToFrom(Vector2 position)
    {
        return ((Vector2) transform.position - position).normalized;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
