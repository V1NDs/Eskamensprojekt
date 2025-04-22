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

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
}
