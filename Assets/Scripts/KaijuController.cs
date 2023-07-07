using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KaijuController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private InputAction playerMovement;
    [SerializeField]
    private InputAction dash;
    [SerializeField]
    private InputAction melee;

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


    private Vector2 movementDirection = new Vector2();
    private bool dashing;
    private bool meleeing;
    private float currentSpeed;

    #region Enabling and disabling controls on enable and disable
    void OnEnable() 
    {
        playerMovement.Enable();
        dash.Enable();
        melee.Enable();
    }

    void OnDisable()
    {
        playerMovement.Disable();
        dash.Disable();
        melee.Disable();
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
        movementDirection = playerMovement.ReadValue<Vector2>();
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
    }

    void FixedUpdate()
    {
        rigidBody.velocity = movementDirection * currentSpeed;
    }

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
}