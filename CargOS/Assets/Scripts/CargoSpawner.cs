﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoSpawner : MonoBehaviour {
    public GameObject shipment;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Spawn()
    {
        GameObject newCargo = Instantiate(shipment, transform.position, transform.rotation);
        newCargo.GetComponent<Grabbable>().RollType();
    }

}
