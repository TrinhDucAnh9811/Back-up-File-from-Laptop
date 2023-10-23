using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : Singleton<Enemies>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float weaponSpeed;
    [SerializeField] private float e_currentHealth;
    [SerializeField] private float e_maxHealth;
    [SerializeField] private int enemyWeaponIndex;  //Chỉ số quyết định mỗi loại Enemy sẽ bắn ra Projectile nào: 0,1,2
    private Vector3 moveDirection;
    private Rigidbody ememyRb;
    private NavMeshAgent enemy;

    private DropManager dropManager;


    //New Code:
    public GameObject e_projectile;
    public LayerMask whatisGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        e_currentHealth = e_maxHealth;
        ememyRb = GetComponent<Rigidbody>();
        enemy = GetComponent<NavMeshAgent>();

        dropManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<DropManager>();
    }

    void Update()
    {

        //New Code:
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void OnCollisionEnter(Collision other) //Khi Enemy bị trúng đạn   (Projectile đang để trigger)
    {
        if (other.gameObject.CompareTag("Player_Weapon"))
        {
            /*Destroy(other.gameObject);*/
            other.gameObject.SetActive(false);
            TakeDamage(PlayerStats.instance.baseDamage);
            Instantiate(PlayerController.instance.GetComponent<Shooting>().hitParticle, transform.position, Quaternion.identity);  //Partical when hit

            AudioManager.instance.PlayTargetSound(2); ////SSSSSSSSSSSSSSSSSSSSS
        }
    }

    private void OnTriggerStay(Collider other) //FIRE THROWER
    {
        if (other.gameObject.CompareTag("Player_Weapon"))
        {
            /*Destroy(other.gameObject);*/
            TakeDamage(PlayerStats.instance.baseDamage);
            Instantiate(PlayerController.instance.GetComponent<Shooting>().hitParticle, transform.position, Quaternion.identity);  //Partical when hit

            AudioManager.instance.PlayTargetSound(2); ////SSSSSSSSSSSSSSSSSSSSS
        }
    }
    public void TakeDamage(int damage) //Sau này sẽ là hàm override từ class Living Entity
    {
        e_currentHealth -= damage;
        if (e_currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            enemy.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached:
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
            walkPointSet = true;

    }
    void ChasePlayer()
    {
        enemy.SetDestination(PlayerController.instance.transform.position);
    }

    void AttackPlayer()
    {
        //Make sure enemy doesn't move
        enemy.SetDestination(transform.position);

        transform.LookAt(PlayerController.instance.transform);

        if (!alreadyAttacked)
        {
            //Attack code here
            
            if (gameObject.name == "Enemy0(Clone)") //CHÚ Ý: KHI ĐỔI TÊN, DẪN ĐẾN KHÔNG BẮN ĐƯỢC
            {
                EnemyShoot(0);
                

            }
            if(gameObject.name == "Enemy1(Clone)")
            {
                EnemyShoot(1);
                
            }
            if(gameObject.name == "Enemy2(Clone)")
            {
                EnemyShoot(2);
                EnemyShoot(3); //Gọi cả Particle Effect của nó
                /*Rigidbody enemyRb = Instantiate(e_projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();*/
                
            }

            if (gameObject.name == "BOSS(Clone)")
            {
                EnemyShoot(1);
                EnemyShoot(3);
            }


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void EnemyShoot(int index)
    {
        GameObject e_projectile = EnemyWeaponPool.instance.GetPooledEnemyWeapon(index = enemyWeaponIndex);
        if (e_projectile != null)
        {
            Instantiate(e_projectile, transform.position, Quaternion.identity);
            e_projectile.transform.position = transform.position;
            e_projectile.transform.rotation = transform.rotation;

            e_projectile.SetActive(true);

            Rigidbody projectileRb = e_projectile.GetComponent<Rigidbody>();

            switch(index)
            {
                case 0:
                    {
                        projectileRb.velocity = transform.forward * weaponSpeed * 15f;
                        StartCoroutine(AutoDestruct(e_projectile, 2f));
                        break;
                    }
                case 1:
                    {
                        projectileRb.velocity = transform.forward * weaponSpeed * 60f;
                        StartCoroutine(AutoDestruct(e_projectile, 2f));
                        break;
                    }
                case 2:
                    {
                        /*projectileRb.AddForce(transform.forward * weaponSpeed * 300f, ForceMode.Impulse);
                        projectileRb.AddForce(transform.up * weaponSpeed * 700, ForceMode.Impulse);*/
                        projectileRb.velocity = transform.forward * weaponSpeed * 20f;
                        StartCoroutine(AutoDestruct(e_projectile, 5f));
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(AutoDestruct(e_projectile, 2f));
                        break;
                    }
                case 4:
                    {
                        StartCoroutine(AutoDestruct(e_projectile, 3f));
                        break;
                    }


            }
            //TEST (Đang xóa thử)
            /*Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = transform.forward * weaponSpeed;  //Bắn theo hướng thẳng của firepoint*/


        }

        IEnumerator AutoDestruct(GameObject x, float time)
        {
            yield return new WaitForSeconds(time);
            x.SetActive(false);
            e_projectile.transform.position = new Vector3(0, 0, 0);
            e_projectile.transform.rotation = Quaternion.identity;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /*public void TakeDamage(int damage)
    {
        e_currentHealth -= damage;

        if (e_currentHealth <= 0)
        {
            Invoke(nameof(DestroyEnemy), 5f);
        }
    }*/

    private void DestroyEnemy()
    {
        //Dead animation + Sound
        gameObject.SetActive(false); //Set inactive
        dropManager.OnEnemyDiactivation();

    }

}



