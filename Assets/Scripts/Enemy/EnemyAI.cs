using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Detection Settings")]
    public float detectionRange = 15f;
    public float attackRange = 2.2f;

    [Header("Attack Settings")]
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;

    [Header("Electric Theme")]
    public GameObject electricAttackEffect;

    private NavMeshAgent agent;
    private float nextAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void StopMoving()
    {
        agent.isStopped = true;
    }

    private void AttackPlayer()
    {
        agent.isStopped = true;

        LookAtPlayer();

        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Enemy attacked player with electric shock.");

            if (electricAttackEffect != null)
            {
                Instantiate(electricAttackEffect, transform.position + transform.forward, Quaternion.identity);
            }

            // Later we will connect this to your PlayerHealth script.
            // For now this proves the enemy can attack.
            
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8f);
        }
    }
}