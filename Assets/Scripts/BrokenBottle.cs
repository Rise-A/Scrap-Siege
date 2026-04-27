using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBottle : MonoBehaviour
{
    public AudioSource bottle;
    // Start is called before the first frame update
    void Start()
    {
        bottle = GetComponent<AudioSource>();
        bottle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
