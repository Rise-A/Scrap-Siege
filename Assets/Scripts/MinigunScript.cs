using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunScript : MonoBehaviour
{
    public GameObject minigun;
    public GameObject bullet;
    public GameObject rocket;
    public Transform bulletFirePoint;
    public Transform rocketFirePoint;
    public float bulletLife;
    public float rocketLife;
    public float bulletCooldown;
    public float rocketCooldown;
    public float bulletVelocity;
    public float rocketVelocity;
    bool readyToFireBullet;
    bool readyToFireRocket;

    // Start is called before the first frame update
    void Start()
    {
        readyToFireBullet = true;
        readyToFireRocket = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && readyToFireBullet){
            readyToFireBullet = false;
            ShootBullet();
            Invoke(nameof(ResetBullet), bulletCooldown);
        }

        if (Input.GetButton("Fire1") && readyToFireRocket){
            readyToFireRocket = false;
            ShootRocket();
            Invoke(nameof(ResetRocket), rocketCooldown);
        }
    }

    private void ShootBullet(){
        GameObject bulletProjectile = Instantiate(bullet, bulletFirePoint.position, bulletFirePoint.rotation);
        bulletProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, bulletVelocity, 0));

        Destroy(bulletProjectile, bulletLife);
    }

    private void ResetBullet(){
        readyToFireBullet = true;
    }

    private void ShootRocket(){
        GameObject rocketProjectile = Instantiate(rocket, rocketFirePoint.position, rocketFirePoint.rotation);
        rocketProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, rocketVelocity, 0));

        Destroy(rocketProjectile, rocketLife);
    }
    
    private void ResetRocket(){
        readyToFireRocket = true;
    }
}
