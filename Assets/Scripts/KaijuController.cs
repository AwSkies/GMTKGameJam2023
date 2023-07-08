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
    #endregion

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject slash;
    [SerializeField]
    private Animator slashAnimator;

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
    #endregion

    #region Projectile attack parameters
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireCooldown;
    #endregion

    private Vector2 movementDirection = new Vector2();
    private Vector2 aimDirection = new Vector2();
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
        rigidBody.velocity = movementDirection * currentSpeed;
    }

    #region Input action listeners
    public void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }

    public void OnAim(InputValue value)
    {
        Vector2 aim = value.Get<Vector2>();
        // Correct if the scheme is KBM
        if (playerInput.currentControlScheme == "KBM")
        {
            aim = Camera.main.ScreenToWorldPoint(aim);
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
            slash.transform.localPosition = aimDirection.normalized;
            slash.SetActive(true);
            Invoke("ResetMeleeCooldown", meleeCooldown);
        }
    }

    public void OnFire()
    {
        if (!firing)
        {
            firing = true;
            projectileFirer.Fire(projectilePrefab, aimDirection);
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