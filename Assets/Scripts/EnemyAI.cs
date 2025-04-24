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

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead) return;

        attackRange = Physics.CheckSphere(transform.position, 1.0f, isPlayer);

        // Follow player
        agent.SetDestination(player.position);

        // If close to attack, stop and damage
        if (attackRange)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!recentlyAttacked)
            {
                // Damage
                print("Attack");

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
        health -= damage;

        if (health < 0)
        {
            isDead = true;
            Invoke("Destroy", 0f);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);

        GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().totalEnemiesKilled += 1;
    }
}
