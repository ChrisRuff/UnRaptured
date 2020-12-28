using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Movement Instance Variables
    //normal movement
    private float speed;
    private Vector3 moveDir;
    private bool isMoving;

    //sprinting
    private bool isSprinting;
    private bool recoverStamina;
    private float staminaPoints;
    public Slider staminaSlider;
    private float timer;

    //jumping
    private Vector3 groundNormal;
    private float groundCheckDistance;
    private float origGroundCheckDistance;
    private bool isGrounded;
    private float jumpHeight;
    #endregion
    #region Health Instance Variables
    public Image damageImage;
    public Slider healthSlider;
    private int startingHealth = 100;
    private int currentHealth;
    private float flashSpeed = 5f;
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private bool isDead;
    private bool damaged;
    #endregion
    public Weapon weapon;
    //comment later
    Rigidbody rb;
    public CameraController playerCameraController;

    // Start is called before the first frame update
    void Start()
    {
        #region Movement
        //normal movement
        speed = 500.0f;
        moveDir = Vector3.zero;
        isMoving = false;

        //sprinting
        isSprinting = false;
        staminaPoints = 100f;
        recoverStamina = false;
        timer = 0.0f;

        //jumping
        isGrounded = false;
        groundCheckDistance = 0.1f;
        origGroundCheckDistance = groundCheckDistance;
        jumpHeight = 50f;
        #endregion
        #region Health
        currentHealth = startingHealth;
        #endregion
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        HandlePlayerHealth();
        HandleShooting();
    }




    #region MOVEMENTBLOCK





    private void HandlePlayerMovement()
    {
        //Check if can jump and jump if able
        CheckIfGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (staminaPoints >= 10)
            {
                Jump();
                staminaPoints -= 10;
                recoverStamina = false;
                timer = 0;
            }
        }

        //sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = true;
            recoverStamina = false;
            timer = 0;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = false;
        }

        if (!isSprinting && isGrounded)
        {
            timer+= Time.deltaTime;
        }

        if (timer > 1)
        {
            recoverStamina = true;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        if (v != 0 && h == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = ray.direction.normalized * speed;

            moveDir = new Vector3(rayDirection.x * v, 0f, rayDirection.z * v); ;
            isMoving = true;
        }
        else if (h != 0 && v == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = ray.direction.normalized * speed;

            moveDir = new Vector3(rayDirection.z * h, 0f, -rayDirection.x * h);
            isMoving = true;
        }
        else if (v != 0 && h != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = ray.direction.normalized * speed;

            moveDir = new Vector3(rayDirection.x * v + rayDirection.z * h, 0f, rayDirection.z * v - rayDirection.x * h);
            isMoving = true;
        }
        else
        {
            moveDir = Vector3.zero;
            isMoving = false;
        }

        if (isMoving && !isDead)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        Rotate();

        if (recoverStamina && staminaPoints < 100)
        {
            staminaPoints++;
        }
        staminaSlider.value = staminaPoints;
    }

    public void Move()
    {
        moveDir = moveDir.normalized;
        Vector3 movementResult = new Vector3();
        if (isSprinting && staminaPoints > 0)
        {
            movementResult = moveDir * speed * 2 * Time.deltaTime;
            movementResult = new Vector3(movementResult.x, rb.velocity.y, movementResult.z);
            rb.velocity = movementResult;
            staminaPoints--;
            
        }
        else
        {
            movementResult = moveDir * speed * Time.deltaTime;
            movementResult = new Vector3(movementResult.x, rb.velocity.y, movementResult.z);
            rb.velocity = movementResult;
        }
    }
    
    private void CheckIfGrounded()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        //Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 30));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 1.2f))
        {
            isGrounded = true;
            groundNormal = hitInfo.normal;
            //m_Animator.applyRootMotion = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            //m_Animator.applyRootMotion = false;
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 10 * jumpHeight * Time.deltaTime, rb.velocity.z);
    }

    public void Rotate()
    {
        rb.transform.rotation = playerCameraController.transform.rotation;
    }
    #endregion END OF MOVMENT BLCOK

    #region PLAYERHEALTHBLOCK



    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }

    }

    private void HandlePlayerHealth()
    {
        Test();
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
        if (isDead)
        {
            RestartLevel();
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;

        playerCameraController.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    #endregion


    #region SHOOTINGBLOCK
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Attack();
        }
    }
    #endregion
}
