
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] ParticleSystem Missile;
    [SerializeField] public float damage = 40f;
    [SerializeField] Canvas e_Turn;

    [SerializeField] GameObject enemyCamera;
    [SerializeField] private AudioClip cannonSound;
    [SerializeField] private AudioClip movingSound;
    [SerializeField] private Camera primaryCamera;
    [SerializeField] public CanvasGroup enemyTurnFade;
    [SerializeField] GameObject enemyMCamera;
    [SerializeField] GameObject ArmatoMissileCamera;
    [SerializeField] GameObject ChiMissileCamera;
    [SerializeField] GameObject CromMissileCamera;

    [SerializeField] GameObject KVMissileCamera;
    public NavMeshAgent agent;
    BoxCollider boxCollider;
    PlayerHealth playerHealth;
    private AudioSource cannonSfx;




    public Transform player;
    float turnSpeed = 5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    EnemyHealth enemyHealth;
    FadeUI fadeUI;
    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking
    // public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool isDead;
    // public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = FindObjectOfType<EnemyHealth>();
        boxCollider = GetComponent<BoxCollider>();
        cannonSfx = GetComponent<AudioSource>();
        fadeUI = GetComponent<FadeUI>();


    }

    private void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        BattleSystem BS = FindObjectOfType<BattleSystem>();
        if (BS.state == BattleState.ENEMYTURN)
        {            // return;
                     // Debug.Log("Its now enemy's turn");
                     // primaryCamera.enabled = false;

            e_Turn.enabled = true;
            Invoke("EnemyFadeUI", 2f);
            searchAndAttack();
            boxCollider.enabled = false;
            enemyCamera.SetActive(true);
            // playerMCamera.SetActive(false);


        }

        else
        {
            e_Turn.enabled = false;
            EnemyFadeInUI();
            enemyCamera.SetActive(false);
            boxCollider.enabled = true;
        }

    }

    public void searchAndAttack()
    {

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange)
            Patroling();




        // if (!playerInSightRange && !playerInAttackRange) Patroling();
        // if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //  if (playerInAttackRange && playerInSightRange) AttackPlayer();
        if (playerInSightRange) Invoke("AttackPlayer", 5f);


    }

    private void Patroling()
    {


        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;

        cannonSfx.clip = movingSound;
        cannonSfx.Play();
    }

    // private void ChasePlayer()
    // {
    //     agent.SetDestination(player.position);
    // }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        FaceTarget();

        // transform.LookAt(player);
        // BattleSystem BS = FindObjectOfType<BattleSystem>();

        if (!Missile.isPlaying && !alreadyAttacked)
        {
            ArmatoMissileCamera.SetActive(false);
            ChiMissileCamera.SetActive(false);
            CromMissileCamera.SetActive(false);
            KVMissileCamera.SetActive(false);
            enemyMCamera.SetActive(true);
            Missile.Play();
            cannonSfx.clip = cannonSound;
            cannonSfx.Play();

            alreadyAttacked = true;
            // Proceed to player's turn

            if (enemyHealth.hitPoints > 1)
            {
                isDead = false;
                Debug.Log("Player's turn starts in 5 seconds");
                BattleSystem BS = FindObjectOfType<BattleSystem>();

                if (BS.state == BattleState.ENEMYTURN)
                {
                    BS.state = BattleState.MISSILE;

                    Invoke("ProceedTurn", 5f);
                }
                else
                {
                    return;
                }


            }


        }
    }


    private void ProceedTurn()
    {
        BattleSystem BS = FindObjectOfType<BattleSystem>();


        BS.state = BattleState.PLAYERTURN;
        alreadyAttacked = false;




    }

    private void EnemyFadeUI()
    {
        enemyTurnFade.alpha -= Time.deltaTime;
    }

    private void EnemyFadeInUI()
    {
        enemyTurnFade.alpha += Time.deltaTime;
    }

    //  if (!alreadyAttacked)
    //  {
    //         ///Attack code here
    //         Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
    //         rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
    //         rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    //         ///End of attack code

    //         alreadyAttacked = true;
    //         Invoke(nameof(ResetAttack), timeBetweenAttacks);




    // private void ResetAttack()
    // {
    //     alreadyAttacked = false;
    // }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;

    //     if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    // }
    // private void DestroyEnemy()
    // {
    //     Destroy(gameObject);
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
    private void FaceTarget()
    {
        //used Vector 3 for 3D directions
        //Quaternion.Slerp(our rotation, target rotation, speed of rotation)
        //Where is the target - Where are we. normalized (When normalized, a vector keeps the same direction but its length is 1.0)
        // Direction x is the first line, 0 is on the y axis.
        //Quaternion.Slerp(Current rotation, look rotation, Time and speed)  
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }


}
