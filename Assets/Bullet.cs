using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }
        if(spread == true)
        {
            targetPoint += new Vector3(Random.Range(-1.4f,1.4f), Random.Range(-1.4f, 1.4f), Random.Range(-1.4f, 1.4f));
        }
        direction = targetPoint - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction.normalized * bulletSpeed;
        if (spread)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.transform.root.gameObject.BroadcastMessage("Damage");
        }
        if (collision.gameObject.tag != "bullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(sparks,transform.position,transform.rotation);
    }
}
