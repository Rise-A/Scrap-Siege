using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameObject rocketProjectile;
    public int damage = 100;
    public float explosionRadius = 5;
    public ParticleSystem explosion;
    // public float bottleBreakPowerMax = 1000;
    // public float bottleBreakRadius = 1000;
    public AudioSource rocketShoot;

    void Start(){
        rocketShoot = GetComponent<AudioSource>();
        rocketShoot.Play();
    }

    void Update(){
        transform.position = rocketProjectile.transform.position;
        // for arrows VV
        //transform.LookAt(transform.position + bottle.GetComponent<Rigidbody>().velocity/2);
    }
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Ground"){
            Explode();
        }

        if (collision.gameObject.tag == "Gobbler"){
            Explode();
        }

        if (collision.gameObject.tag == "Scrapion"){
            Explode();
        }
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in colliders){
            Gobbler gobbler = c.gameObject.GetComponent<Gobbler>(); // Get the Gobbler component from the collision object

            if (gobbler != null){
                ParticleSystem exp = Instantiate(explosion, transform.position, Quaternion.Euler(-90,0,0));
                gobbler.TakeDamage(damage);
                Destroy(rocketProjectile); 
            }

            ScrapionScript scrapion = c.gameObject.GetComponent<ScrapionScript>();

            if (scrapion != null){
                ParticleSystem exp = Instantiate(explosion, transform.position, Quaternion.Euler(-90,0,0));
                scrapion.TakeDamage(damage);
                Destroy(rocketProjectile); 
            }


            ParticleSystem exp1 = Instantiate(explosion, transform.position, Quaternion.Euler(-90,0,0));
            Destroy(rocketProjectile);
        }
    }
}
