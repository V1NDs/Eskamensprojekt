using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;
    public float attackSpeed = 2f;
    private bool recentlyAttacked = false;
    private bool attackRange = false;
    public float health = 100f;
    public bool isDead;
    public string type;
    public int damage = 20;
    public Animator mAnimator;
    public ParticleSystem bloodEffectPrefab;  // Tildel dit blod-partikel prefab i inspector
    public GameObject blood;

    private void Awake()
    {
        mAnimator = mAnimator.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead) return;

        if (type == "melee")
        {
            attackRange = Physics.CheckSphere(transform.position, 1.0f, isPlayer);
        }

        if (type == "stink")
        {
            attackRange = Physics.CheckSphere(transform.position, 5.0f, isPlayer);
        }

        // Follow player
        agent.SetDestination(player.position);

        // If close to attack, stop and damage
        if (attackRange)
        {
            if (type == "melee")
            {
                agent.SetDestination(transform.position);
                transform.LookAt(player);
            }

            if (!recentlyAttacked && !isDead)
            {
                // Damage
                GameObject.Find("Player").GetComponent<Health>().TakeDamage(damage);
                GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().ChangeDifficulty(false);

                recentlyAttacked = true;
                Invoke(nameof(ResetAttack), attackSpeed);
            }
        }
    }

    private void ResetAttack()
    {
        recentlyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        mAnimator.SetTrigger("Damage");

        if (health < 0)
        {
            mAnimator.SetBool("Death", true);
            agent.isStopped = true;
            Vector3 coords = transform.position;
            coords.y -= 0.95f;

            // Spawn blood
            GameObject bloodStain = Instantiate(blood, coords, Quaternion.identity);

            isDead = true;
            Invoke("Destroy", 2.5f);
            Destroy(bloodStain, 30f);

            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().ChangeDifficulty(true);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);

        GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().totalEnemiesKilled += 1;
    }

}
