﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crain : MonoBehaviour {
    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    Wench wench;
	// Use this for initialization
	void Start () {
        wench = GetComponentInChildren<Wench>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving == true && transform.position.x != targetPosition.x)
        {
            if(transform.position.x < targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.x);
            }
            else if(transform.position.x > targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.x);
            }
            if (Mathf.Abs(transform.position.x - targetPosition.x) < .01)
            {
                wench.targetPosition = targetPosition;
                wench.isMoving = true;
                isMoving = false;
            }
        }
        
	}
}