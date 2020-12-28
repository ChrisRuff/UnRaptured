using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// Health
	public int health;

	//normal movement
	private float speed;
	private Vector3 moveDir;
	private bool isMoving;

	//sprinting
	private bool isSprinting;
	private bool recoverStamina;
	private float staminaPoints;

	//jumping
	private Vector3 groundNormal;
	private float groundCheckDistance;
	private float origGroundCheckDistance;
	private bool isGrounded;
	private float jumpHeight;

	//comment later
	Rigidbody rb;
	public CameraController playerCameraController;

	// Start is called before the first frame update
	void Start()
	{
		
		//normal movement
		speed = 500.0f;
		moveDir = Vector3.zero;
		isMoving = false;

		//sprinting
		isSprinting = false;
		staminaPoints = 100000f;
		recoverStamina = false;

		//jumping
		isGrounded = false;
		groundCheckDistance = 0.1f;
		origGroundCheckDistance = groundCheckDistance;
		jumpHeight = 50f;

		rb = GetComponent<Rigidbody>(); 
	}

	// Update is called once per frame
	void Update()
	{
		if(health <= 0)
		{
			// Dead
			Debug.Log("Dead");
		}

		//Check if can jump and jump if able
		CheckIfGrounded();
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			if (staminaPoints >= 10)
			{
				Jump();
				staminaPoints -= 10;
				recoverStamina = false;
			}
		}

		//sprinting
		if (Input.GetKey(KeyCode.LeftShift))
		{
			isSprinting = true;
		}
		else
		{
			isSprinting = false;
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

		if (isMoving)
		{
			Move();
		}
		else
		{
			rb.velocity = Vector3.zero;
		}

		Rotate();
	}

	public void Move()
	{
		moveDir = moveDir.normalized;
		if (isSprinting && staminaPoints > 0)
		{
			rb.velocity = moveDir * speed * 2 * Time.deltaTime;
			staminaPoints--;
			
		}
		else
		{
			rb.velocity = moveDir * speed * Time.deltaTime;
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
		rb.velocity = new Vector3(0, 10 * jumpHeight * Time.deltaTime, 0);
	}

	public void Rotate()
	{
		rb.transform.rotation = playerCameraController.transform.rotation;
	}

	public void Hit(int damage)
	{
		health -= damage;
		Debug.Log("Took " + damage + " damage!");
	}

}
