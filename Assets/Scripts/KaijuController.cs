using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float fireCooldown;
    #endregion

    private System.Random rng = new System.Random();
    private Vector2 movementDirection = new Vector2();
    private Vector2 aimDirection = new Vector2();
    private Vector2 actionDirection = new Vector2();
    private Vector2 lastDirection = new Vector2();
    private float currentSpeed;
    private bool dashing;
    private bool meleeing;
    private bool firing;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
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
        // Correct if the scheme is KBM
        if (playerInput.currentControlScheme == "KBM")
        {
            aim = Camera.main.ScreenToWorldPoint(aim) - transform.position;
        }
        if (aim != Vector2.zero)
        {
            aimDirection = aim.normalized;
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
            actionDirection = aimDirection;
            slash.transform.position = aimDirection * meleeRadius + new Vector2(transform.position.x, transform.position.y);
            slash.transform.localRotation = Quaternion.AngleAxis(-Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg + (float) rng.NextDouble() * meleeRotationRandomizationRange, Vector3.forward);
            slash.SetActive(true);
            Invoke("ResetMeleeCooldown", meleeCooldown);
        }
    }

    public void OnFire()
    {
        if (!firing)
        {
            firing = true;
            actionDirection = aimDirection;
            projectileFirer.FireInDirection(projectilePrefab, aimDirection);
            Invoke("ResetFireCooldown", fireCooldown);
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
    }

    private void ResetMeleeCooldown()
    {
        meleeing = false;
    }

    private void ResetFireCooldown()
    {
        firing = false;
    }
    #endregion
}