using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float speed;
    private Vector3 moveDir;
    private bool isMoving;

    private bool isSprinting;
    private bool recoverStamina;
    private float staminaPoints;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15.0f;
        moveDir = Vector3.zero;
        isMoving = false;
        isSprinting = false;
        staminaPoints = 100000f;
        recoverStamina = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        
        if (v != 0 && h == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = rayDirection = ray.direction.normalized * 80 - new Vector3(-(transform.position.x - Camera.main.transform.position.x), -(transform.position.y - Camera.main.transform.position.y), -(transform.position.z - Camera.main.transform.position.z));

            moveDir = new Vector3(rayDirection.x * v, 0f, rayDirection.z * v); ;
            isMoving = true;
        }
        else if (h != 0 && v == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = rayDirection = ray.direction.normalized * 80 - new Vector3(-(transform.position.x - Camera.main.transform.position.x), -(transform.position.y - Camera.main.transform.position.y), -(transform.position.z - Camera.main.transform.position.z));
            moveDir = new Vector3(rayDirection.z * h, 0f, -rayDirection.x * h);
            isMoving = true;
        }
        else if (v != 0 && h != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 rayDirection = rayDirection = ray.direction.normalized * 80 - new Vector3(-(transform.position.x - Camera.main.transform.position.x), -(transform.position.y - Camera.main.transform.position.y), -(transform.position.z - Camera.main.transform.position.z));
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
    }

    public void Move()
    {
        moveDir = moveDir.normalized;
        if (isSprinting && staminaPoints > 0)
        {
            transform.position = transform.position + (moveDir * speed * 2 * Time.deltaTime);
            staminaPoints--;
            
        }
        else
        {
            transform.position = transform.position + (moveDir * speed * Time.deltaTime);
        }
    }
}
