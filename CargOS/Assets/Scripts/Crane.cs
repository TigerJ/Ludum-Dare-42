using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {
    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    Winch winch;
	// Use this for initialization
	void Start () {
        winch = GetComponentInChildren<Winch>();
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
            if (Mathf.Abs(transform.position.x - targetPosition.x) < .01)
            {
                winch.targetPosition = targetPosition;
                winch.isMoving = true;
                isMoving = false;
            }
        }
        
	}
}