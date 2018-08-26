using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {
    public List<Vector3> targets;
    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    public string direction;
    public string directionY;
    public Winch winch;
    Vector3 velo;
	// Use this for initialization
	void Start () {
        winch = GetComponentInChildren<Winch>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving)
        {
            Vector3 dampTarget = new Vector3(targetPosition.x, transform.position.y, 0);
            //transform.position = Vector3.SmoothDamp(transform.position, dampTarget, ref velo, .4f);
            transform.position = Vector3.MoveTowards(transform.position, dampTarget, .04f);
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
            // target less transform will always be positive

            if (Mathf.Abs(targetPosition.x)-Mathf.Abs(transform.position.x) <.01 && Mathf.Abs(targetPosition.x) - Mathf.Abs(transform.position.x) > -.01)
            {
                winch.targetPosition = targetPosition;
                winch.isMoving = true;
                isMoving = false;
            }
        }
        
	}
    public void checkTargets()
    {
        if (targets.Count > 0)
        {
            targetPosition = targets[0];
            isMoving = true;
        }
    }
}