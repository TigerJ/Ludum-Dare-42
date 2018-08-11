using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wench : MonoBehaviour {

    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    public bool overGrabbable;
    public GameObject grabbaleObject;
    public GameObject grabbedObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true && transform.position.y != targetPosition.y)
        {
            if (transform.position.y < targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.y);
            }
            else if (transform.position.y > targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.y);
            }
            if (grabbedObject != null)
            {
                grabbedObject.transform.position = transform.position;
            }
            if (Mathf.Abs(transform.position.y - targetPosition.y) < .04)
            {
                if (overGrabbable == true && grabbedObject == null)
                {
                    grabbedObject = grabbaleObject;
                    grabbedObject.GetComponent<Grabbable>().grabbed = true;
                    grabbedObject.GetComponent<Grabbable>().beltMovement = false;
                }
                else if(grabbedObject!=null)
                {
                    grabbedObject.GetComponent<Grabbable>().grabbed = false;
                    grabbedObject.GetComponent<Grabbable>().beltMovement = false;
                    grabbedObject = null;
                }
                isMoving = false;
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabbable")
        {
            grabbaleObject = collision.gameObject;
            overGrabbable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabbable")
        {
            overGrabbable = false;
        }
    }
}
