using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject bulletProjectile;
    public int damage = 100;
    // public float bottleBreakPowerMax = 1000;
    // public float bottleBreakRadius = 1000;
    public AudioSource bulletSound;

    void Start(){
        bulletSound = GetComponent<AudioSource>();
        bulletSound.Play();
    }

    void Update(){
        transform.position = bulletProjectile.transform.position;
        // for arrows VV
        //transform.LookAt(transform.position + bottle.GetComponent<Rigidbody>().velocity/2);
    }
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Ground"){
            Destroy(bulletProjectile);
        }

        Gobbler gobbler = collision.gameObject.GetComponent<Gobbler>(); // Get the Gobbler component from the collision object

        if (gobbler != null){
            gobbler.TakeDamage(damage);
            Destroy(bulletProjectile); 
        }

        ScrapionScript scrapion = collision.gameObject.GetComponent<ScrapionScript>(); // Get the Gobbler component from the collision object

        if (scrapion != null){
            scrapion.TakeDamage(damage);
            Destroy(bulletProjectile); 
        }
    }
}
