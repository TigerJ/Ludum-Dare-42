using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {
    public GameObject ship;
    public bool takeTurn;
    int genCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(takeTurn == true)
        {
            takeTurn = false;
            genCount++;
            if (genCount == 3)
            {
                Instantiate(ship, transform.position, transform.rotation);
                genCount = 0;
            }
        }
	}
    
}
