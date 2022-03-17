using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public class Weapon
    {
        public int maxAmmo;
        public int clipSize;
        public float timeBetween;
        public int damage;

        public Weapon(int maxAmmo, int clipSize, float timeBetween, int damage)
        {
            this.maxAmmo = maxAmmo;
            this.clipSize = clipSize;
            this.timeBetween = timeBetween;
            this.damage = damage;
        }
    }

    public GameObject bullet;
    public GameObject spawn;
    public Camera cam;
    Weapon pistol;
    Weapon shotgun;
    Weapon rifle;
    Weapon currentWeapon;

    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        pistol = new Weapon(50,10,0.6f,15);
        shotgun = new Weapon(50, 6, 1.1f, 5);
        rifle = new Weapon(400, 40, 0.2f, 11);
        Debug.Log(pistol.damage);
        currentWeapon = pistol;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = pistol;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = shotgun;
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            if(currentWeapon == shotgun)
            {
                Instantiate(bullet, spawn.transform.position, cam.transform.rotation);
                Instantiate(bullet, spawn.transform.position, cam.transform.rotation * Quaternion.LookRotation(spawn.transform.forward + new Vector3(-5f, 5f, 0f)));
                Instantiate(bullet, spawn.transform.position, cam.transform.rotation * Quaternion.LookRotation(spawn.transform.forward + new Vector3(5f, -5f, 0f)));
            }
            else
            {
                Instantiate(bullet,spawn.transform.position,cam.transform.rotation);
            }
            canShoot = false;
            Invoke("enable", currentWeapon.timeBetween);
        }
    }

    void enable()
    {
        canShoot = true;
    }

}
