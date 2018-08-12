using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {
    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    public string direction;
    public string directionY;
    public Winch winch;
	// Use this for initialization
	void Start () {
        winch = GetComponentInChildren<Winch>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving)
        {
            if(transform.position.x < targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.x);
            }
            else if(transform.position.x > targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.x);
            }
            if (winch.grabbedObject != null)
            {
                if (winch.parentContainer == null)
                {
                    winch.grabbedObject.transform.position = winch.transform.position;
                }
                else
                {
                    winch.grabbedObject.transform.parent.position = transform.position - winch.grabbedObject.transform.localPosition;
                }
            }
            if ((direction == "left" && transform.position.x <= targetPosition.x) || direction == "right" && transform.position.x >= targetPosition.x)
            {
                winch.targetPosition = targetPosition;
                winch.isMoving = true;
                isMoving = false;
            }
        }
        
	}
}