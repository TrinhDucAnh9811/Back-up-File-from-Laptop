using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    LineRenderer lineRenderer;

    public float horizontalInput;
    public float verticalInput;

    private Rigidbody playerRb;
    public Vector3 reference_Weapon;  //Để Vũ khí có thể biết hướng đi của nhân vật mà phóng theo hướng đó

    public GameObject bombPrefab; //The bomb has already attached Script "Bomb"
    //Aim variables:
    public Vector3 mousePos;
    public Transform firePoint; //Vị trí tạo skill: Bomb

    //Dashing variables:
    private bool isDashing;
    [SerializeField] private float dashSpeed = 30;//Sửa ngoài Inspector
    [SerializeField] private float dashTime = 0.25f;
    private float lastDashTime = 0.0f;
    private bool canDash = true;



    void Start()
    {
       lineRenderer = GetComponent<LineRenderer>();

       playerRb = GetComponent<Rigidbody>();

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) { return; }
        //Movement:
        PlayerMovement();

        //Aiming:
        int layer = 1 << 5 | 1 << 9;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layer))
        {
            mousePos = hit.point;
        }


        //Dashing:
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastDashTime > dashTime && canDash == true)
        {
            StartCoroutine(Dash(PlayerStats.instance.dashCoolDown));
            lastDashTime = Time.time; // Lưu thời điểm cuối cùng dash
            PlayerStats.instance.dashCoolDown -= Time.deltaTime;
        }
    }

    IEnumerator Dash(float time)
    {
        canDash = false;
        isDashing = true;
        playerRb.velocity = new Vector3(horizontalInput, 0, verticalInput).normalized * dashSpeed;

        yield return new WaitForSeconds(dashTime); // Chờ cho khoảng thời gian dash
        playerRb.velocity = Vector3.zero; // Dừng nhân vật

        yield return new WaitForSeconds(0.0001f); // Chờ thời gian còn lại của cooldown
        isDashing = false;
        canDash = false;

        yield return new WaitForSeconds(PlayerStats.instance.dashCoolDown);
        canDash= true;
    }


    private void FixedUpdate()
    {
        Vector3 targetPos = mousePos;
        targetPos.y = transform.position.y; //Match player's y-position
        Vector3 lookDirection = targetPos - transform.position;
        lookDirection.y = 0; // Đảm bảo rằng không có sự xoay theo trục y
        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            playerRb.MoveRotation(rotation);
        }




        //Laser line:
        // Thiết lập đỉnh đầu của tia laser (nguồn)
        lineRenderer.SetPosition(0, firePoint.position);

        // Thiết lập đỉnh cuối của tia laser (điểm hit)
        lineRenderer.SetPosition(1, (mousePos-transform.position)*1000);

    }

   
    private void PlayerMovement()
    {
        //"Player Movement:
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.velocity = new Vector3(horizontalInput, 0, verticalInput) * PlayerStats.instance.moveSpeed;

        reference_Weapon = playerRb.velocity;

        //Sound:
       /* AudioManager.instance.PlayTargetSound(0);*/
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Healing"))
        {
            PlayerStats.instance.currentHealth += PlayerStats.instance.healAmount * Time.deltaTime;
            if(PlayerStats.instance.currentHealth >= PlayerStats.instance.maxHealth)
            { 
                PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth; 
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            other.gameObject.SetActive(false);
            //Thêm hiệu ứng vào đây
        }

        if (other.CompareTag("Exp1"))
        {
            other.gameObject.SetActive(false); //Sau này sẽ làm ObjectPooling
            PlayerStats.instance.IncreaseExperience(2);
            //Thêm hiệu ứng vào đây
            AudioManager.instance.PlayTargetSound(10);
        }
        if (other.CompareTag("Exp2"))
        {
            other.gameObject.SetActive(false); //Sau này sẽ làm ObjectPooling
            PlayerStats.instance.IncreaseExperience(10);
            AudioManager.instance.PlayTargetSound(10);
        }
        if (other.CompareTag("Exp3"))
        {
            other.gameObject.SetActive(false); //Sau này sẽ làm ObjectPooling
            PlayerStats.instance.IncreaseExperience(100);
            AudioManager.instance.PlayTargetSound(10);
        }
        if (other.CompareTag("Bonus"))
        {
            other.gameObject.SetActive(false);
            //Thêm hiệu ứng vào đây
        }

        
    }
    }




