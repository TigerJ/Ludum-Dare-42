using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    public AudioSource sfx_spike;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Grabbable")
        {
            sfx_spike.Play();
            Destroy(collision.gameObject);
            //do bad things here
        }
    }
}
