using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public bool takeTurn = false;
    float newX;
    float newXMod = 6;
    // Use this for initialization
    void Start () {
        newX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(takeTurn == true)
        {
            newX = transform.position.x + newXMod;
            takeTurn = false;
        }
        if(transform.position.x < newX)
        {
            transform.position = new Vector3(transform.position.x + .04f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 38) Destroy(gameObject);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spawner")
        {
            collision.gameObject.GetComponent<CargoSpawner>().Spawn();
            newXMod = 60;
        }
    }
}
