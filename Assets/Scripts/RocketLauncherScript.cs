using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherScript : MonoBehaviour
{
    public GameObject rocket;
    public GameObject rocketLauncher;
    public Transform firePoint;
    public float rocketLife;
    public float fireCooldown = 1f;
    bool readyToFire;
    public float rocketVelocity = 700f;

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
        GameObject rocketProjectile = Instantiate(rocket, firePoint.position, firePoint.rotation);
        rocketProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, rocketVelocity, 0));

        Destroy(rocketProjectile, rocketLife);
    }
    
    private void ResetFire(){
        readyToFire = true;
    }
}
