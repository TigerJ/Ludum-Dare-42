using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    Grabbable parent;
    public string direction;
	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Grabbable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(parent.grabbed == false && collision.tag == "Grabbable")
        {
            
        }
    }
}
