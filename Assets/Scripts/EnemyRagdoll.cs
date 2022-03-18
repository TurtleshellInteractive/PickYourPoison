using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    public Collider[] limbs;
    public Collider mainCollision;
    public Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.ragdoll)
        {
            mainCollision.enabled = false;
            for (int i = 0; i < limbs.Length; i++)
            {
                limbs[i].enabled = true;
            }
        }
    }
}
