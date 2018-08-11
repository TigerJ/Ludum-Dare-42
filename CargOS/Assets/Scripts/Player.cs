using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    string lastMoved;
    public Crain crain;
    float moveTimer = 0;
    float moveSpeed = 0.25f;
    float turnTimer = 3;
    float gameSpeed = 3;
	// Use this for initialization
	void Start () {
		
	}

	void Update () {
        if (Input.GetButtonUp("Left") || Input.GetButtonUp("Right") || Input.GetButtonUp("Up") || Input.GetButtonUp("Down")) moveTimer = 0;
        if (Input.GetButton("Left") && moveTimer <= 0)
        {
            MovePlayer("Left");
            moveTimer = moveSpeed;
        }
        else if (Input.GetButton("Right") && moveTimer <= 0)
        {
            MovePlayer("Right");
            moveTimer = moveSpeed;
        }
        else if (Input.GetButton("Up") && moveTimer <= 0)
        {
            MovePlayer("Up");
            moveTimer = moveSpeed;
        }
        else if (Input.GetButton("Down") && moveTimer <= 0)
        {
            MovePlayer("Down");
            moveTimer = moveSpeed;
        }
        else if (Input.GetButton("Left") || Input.GetButton("Right") || Input.GetButton("Up") || Input.GetButton("Down") && moveTimer > 0)
        {
            moveTimer = moveTimer - Time.deltaTime;
        }
        if (Input.GetButtonDown("Action"))
        {
            crain.targetPosition = new Vector3(transform.position.x + .5f,transform.position.y,transform.position.z);
            crain.isMoving = true;
        }


        if (turnTimer > 0) turnTimer = turnTimer - Time.deltaTime;
        if(turnTimer <=0)
        {
            Debug.Log("turn");
            turnTimer = gameSpeed;
            GameObject[] shipments = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject shipment in shipments) shipment.GetComponent<Grabbable>().activateGrabbable();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if(lastMoved == "Up") transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            if (lastMoved == "Down") transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (lastMoved == "Left") transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            if (lastMoved == "Right") transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
    }
    void MovePlayer(string Direction)
    {
        
        lastMoved = Direction;
        if (Direction == "Up")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (Direction == "Down")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        } 
        if (Direction == "Right")
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        if (Direction == "Left")
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
    }
}
