using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed = 15f;
    Vector3 direction;
    public bool spread = false;
    public GameObject enemy;
    public GameObject sparks;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.forward.normalized;
        rb.velocity = direction.normalized * bulletSpeed;
        if (spread)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.root.gameObject.BroadcastMessage("Damage",20);
        }
        if (collision.gameObject.tag != "bullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(sparks, transform.position, transform.rotation);
    }
}
