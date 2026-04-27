using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject pistol;
    public Transform firePoint;
    public float bulletLife;
    public float fireCooldown = 1f;
    bool readyToFire;
    public float bulletVelocity = 700f;

    void Start(){
        readyToFire = true;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && readyToFire){
            readyToFire = false;
            Shoot();
            Invoke(nameof(ResetFire), fireCooldown);

        }
    }

    private void Shoot(){
        GameObject bulletProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bulletProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, bulletVelocity, 0));

        Destroy(bulletProjectile, bulletLife);
    }
    
    private void ResetFire(){
        readyToFire = true;
    }
}
