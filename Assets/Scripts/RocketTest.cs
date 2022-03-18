using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTest : MonoBehaviour
{
    public SphereCollider radius;
    float explo = 0.5f;
    bool increase = true;
    void Start()
    {
        radius = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(explo <= 0.5f)
        {
            increase = true;
        }
        if(explo >= 20f)
        {
            increase = false;
        }
        if (increase)
        {
            explo += 0.01f;
        }
        else
        {
            explo -= 0.01f;
        }
        radius.radius = explo;
    }
}
