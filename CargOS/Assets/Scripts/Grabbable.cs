using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {
    public bool takeTurn = false;
    public bool grabbed = false;
    public bool onBelt = false;
    bool onTeleporter = false;
    public bool beltMovement = false;
    public float newY;
	// Use this for initialization
	void Start () {
        newY = transform.position.y;
        takeTurn = false;
        onTeleporter = false;
        beltMovement = false;
        grabbed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (takeTurn == true)
        {
            if (onBelt == true)
            {
                beltMovement = true;
                newY = transform.position.y - 1;
            }
            takeTurn = false;
        }

        if (beltMovement = true && transform.position.y > newY && grabbed == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);
        }
        else beltMovement = false;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Belt")
        {
            onBelt = true;
        }
    }
    public void activateGrabbable()
    {
        if (grabbed == false) takeTurn = true;
    }
}
