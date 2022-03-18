using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }

    void Damage(int damage)
    {
        if(damage == 0)
        {
            health -= 12;
            Debug.Log("e");
        }
        else
        {
            health -= damage;
        }
        
    }
}
