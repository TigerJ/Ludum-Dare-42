using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    string lastMoved;
    public Crane crane;
    float moveTimer = 0;
    float moveSpeed = 0.1f;
    float turnTimer = 5;
    float gameSpeed = 5;
    float stopBelts = 0;
    public AudioSource belt_move;
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
            crane.targetPosition = new Vector3(transform.position.x + .5f,transform.position.y,transform.position.z);
            crane.isMoving = true;
            if (crane.transform.position.x >= transform.position.x) crane.direction = "left";
            else crane.direction = "right";
            if (crane.winch.transform.position.y >= transform.position.y) crane.winch.direction = "up";
            else crane.winch.direction = "down";
        }


        if (turnTimer > 0) turnTimer = turnTimer - Time.deltaTime;
        if(turnTimer <=0)
        {
            turnTimer = gameSpeed;
            GameObject[] shipments = GameObject.FindGameObjectsWithTag("Grabbable");
            foreach (GameObject shipment in shipments) shipment.GetComponent<Grabbable>().activateGrabbable();
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
            foreach (GameObject ship in ships) ship.GetComponent<Ship>().takeTurn = true;
            GameObject.Find("ShipSpawner").GetComponent<ShipSpawner>().takeTurn = true;
            GameObject[] belts = GameObject.FindGameObjectsWithTag("Belt");
            foreach (GameObject belt in belts) belt.GetComponent<Animator>().SetBool("move", true);
            stopBelts = 1;
            belt_move.Play();
        }
        if (stopBelts <= 0)
        {
            GameObject[] belts = GameObject.FindGameObjectsWithTag("Belt");
            foreach (GameObject belt in belts) belt.GetComponent<Animator>().SetBool("move", false);
            belt_move.Stop();
        }
        if (stopBelts > 0)
        {
            stopBelts = stopBelts - Time.deltaTime;
            belt_move.pitch = Random.Range(.8f, 1.1f);
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
