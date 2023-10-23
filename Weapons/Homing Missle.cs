using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 300.0f;
    public float rotateSpeed = 200f;
    public float stoppingDistance = 1.0f; // Khoảng cách dừng tên lửa

    private bool isTargetReached = false; // Đã đạt đến mục tiêu chưa

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Nếu đã đạt đến mục tiêu, thì không làm gì cả
        if (isTargetReached)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        // Lấy danh sách tất cả các enemy trong scene
        Enemies[] allEnemies = FindObjectsOfType<Enemies>();

        // Tạo biến lưu trữ enemy gần nhất và khoảng cách gần nhất
        Enemies nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Lặp qua từng enemy để tìm enemy gần nhất
        foreach (Enemies enemy in allEnemies)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float distance = direction.magnitude;

            // Nếu khoảng cách này gần hơn khoảng cách gần nhất hiện tại
            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        // Nếu có enemy gần nhất được tìm thấy
        if (nearestEnemy != null)
        {
            // Tính hướng đến enemy gần nhất
            Vector3 direction = nearestEnemy.transform.position - transform.position;
            direction.Normalize();

            // Tính góc quay để đối diện với hướng mong muốn
            float rotateAmount = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

            // Áp dụng lực xoay để đối diện với hướng mong muốn (trục y hướng lên trên)
            rb.angularVelocity = new Vector3(0, rotateAmount * rotateSpeed, 0);

            // Tính khoảng cách đến enemy gần nhất
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);

            // Nếu khoảng cách nhỏ hơn hoặc bằng khoảng cách dừng tên lửa
            if (distanceToEnemy <= stoppingDistance)
            {
                // Đã đạt đến mục tiêu
                isTargetReached = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                // Áp dụng lực di chuyển theo hướng forward (trục y) với tốc độ mong muốn
                rb.velocity = transform.forward * speed;
            }
        }
        else
        {
            // Nếu không có enemy nào, bạn có thể thực hiện hành động khác hoặc ngừng tên lửa
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            {
            AudioManager.instance.PlayTargetSound(7); ////SSSSSSSSSSSSSSSSSSSSS
        }
    }
}
