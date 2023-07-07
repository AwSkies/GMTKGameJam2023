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

    #region InputAction references
    [SerializeField]
    private InputAction movement;
    [SerializeField]
    private InputAction dash;
    [SerializeField]
    private InputAction melee;
    [SerializeField]
    private InputAction fireDirectionMouse;
    [SerializeField]
    private InputAction fireDirectionController;
    [SerializeField]
    private InputAction fire;
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
    #endregion

    #region Projectile attack parameters
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireCooldown;
    #endregion

    private Vector2 movementDirection = new Vector2();
    private float currentSpeed;
    private bool dashing;
    private bool meleeing;
    private bool firing;

    #region Enabling and disabling controls on enable and disable
    void OnEnable()
    {
        movement.Enable();
        dash.Enable();
        melee.Enable();
        fireDirectionMouse.Enable();
        fireDirectionController.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        dash.Disable();
        melee.Disable();
        fireDirectionMouse.Disable();
        fireDirectionController.Disable();
        fire.Disable();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = movement.ReadValue<Vector2>();
        // Dashing
        if (dash.triggered && !dashing)
        {
            dashing = true;
            currentSpeed = dashSpeed;
            Invoke("ResetMovementSpeed", dashTime);
            Invoke("ResetDashCooldown", dashCooldown);
        }
        // Meleeing
        if (melee.triggered && !meleeing)
        {
            meleeing = true;
            // Do the attack thing here
            Invoke("ResetMeleeCooldown", meleeCooldown);
        }
        // Firing a projectile
        if (fire.triggered && !firing)
        {
            firing = true;
            if (fireDirectionController.ReadValue<Vector2>() == Vector2.zero)
            {
                projectileFirer.FireAt(projectilePrefab, Camera.main.ScreenToWorldPoint(fireDirectionMouse.ReadValue<Vector2>()));
            }
            else
            {
                projectileFirer.Fire(projectilePrefab, fireDirectionController.ReadValue<Vector2>());
            }
            Invoke("ResetFireCooldown", fireCooldown);
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = movementDirection * currentSpeed;
    }

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