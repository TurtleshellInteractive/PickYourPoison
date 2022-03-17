using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public NavMeshAgent agent;
    public Transform player;
    GameObject playerObj;
    public GameObject neck;
    GameObject bones;
    public LayerMask whatIsGround, WhatIsPlayer;
    public Animator animator;

    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkpointRange;
    public int damage;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;
    bool stopped = false;
    bool floating = false;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("player").transform;
        playerObj = GameObject.Find("player");
        GameObject rig = gameObject.transform.Find("Running").gameObject;
        bones = rig.transform.Find("mixamorig:Hips").gameObject;
        neck = bones.transform.Find("mixamorig:Spine").gameObject;
        if (neck == null)
        {
            Debug.Log("uhoh");
        }
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        inSightRange = UnityEngine.Physics.CheckSphere(transform.position,sightRange,WhatIsPlayer);
        inAttackRange = UnityEngine.Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!stopped)
        {
            if (!inSightRange && !inAttackRange) patrolling();
            if (inSightRange && !inAttackRange) chase();
            if (inSightRange && inAttackRange) attack();
        }
    }
    void patrolling()
    {
        if (!walkpointSet) SearchWalkPoint();
        if (walkpointSet)
        {
            agent.SetDestination(walkpoint);
        }
        Vector3 distance = transform.position - walkpoint;
        if (distance.magnitude < 1f)
        {
            walkpointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);
        walkpoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);
        if (UnityEngine.Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkpointSet = true;
        }
    }

    void chase()
    {
        agent.SetDestination(player.position);
    }

    void attack()
    {
        agent.SetDestination(transform.position);
        Transform look = transform;
        look.rotation = player.rotation;
        look.Rotate(0, 180, 0);
        transform.LookAt(look);
        /*Ray ray = new Ray(gameObject.transform.position,gameObject.transform.forward);
        RaycastHit hit;
        UnityEngine.Physics.Raycast(ray, out hit);*/
            if (!alreadyAttacked)
        {
            if (Random.Range(0, 10) < 7)
            {
                playerObj.BroadcastMessage("Damage", damage);
            }
            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks);
        }
    }

    void resetAttack()
    {
        alreadyAttacked = false;
    }
    void Damage()
    {
        animator.enabled = false;
        agent.enabled = false;
        stopped = true;
        Invoke("death", 5f);
    }


    void stop()
    {
        stopped = true;
        Invoke(nameof(resetAi), 5f);
    }

    void resetAi()
    {
        stopped = false;
    }

    void blindEye()
    {
        agent.speed = agent.speed * 1.5f;
    }

    void floater()
    {
        floating = true;
    }

    void hardcore()
    {
        agent.speed *= 2;
        agent.acceleration = agent.acceleration * 1.3f;
    }

    void death()
    {
        Destroy(gameObject);
    }
}
