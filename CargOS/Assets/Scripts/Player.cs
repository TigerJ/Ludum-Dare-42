using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    string lastMoved;
    public Crain crain;
	// Use this for initialization
	void Start () {
		
	}

	void Update () {
        if (Input.GetButtonDown("Left")) MovePlayer("Left");
        if (Input.GetButtonDown("Right")) MovePlayer("Right");
        if (Input.GetButtonDown("Down")) MovePlayer("Down");
        if (Input.GetButtonDown("Up")) MovePlayer("Up");

        if (Input.GetButtonDown("Action"))
        {
            crain.targetPosition = new Vector3(transform.position.x + .5f,transform.position.y,transform.position.z);
            crain.isMoving = true;
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
