using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winch : MonoBehaviour {

    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
    public bool overGrabbable;
    public GameObject grabbaleObject;
    public GameObject grabbedObject;
    public ParentContainer parentContainer;
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
                if(parentContainer == null)
                {
                    grabbedObject.transform.position = transform.position;
                }
                else
                {
                    grabbedObject.transform.parent.position = transform.position - grabbedObject.transform.localPosition;
                }

                //Grabbable grabbedContainer = grabbedObject.GetComponent<Grabbable>();
                //Vector3 connectedPosition;
                //if(grabbedContainer.connectedUp!= null)
                //{
                //    //connectedPosition = 
                //    grabbedContainer.connectedUp.transform.position = grabbedContainer.connectedUp.transform.position + positionChange;
                //} 
            }
            if (Mathf.Abs(transform.position.y - targetPosition.y) < .04)
            {
                Grabbable grabby;
                if (overGrabbable == true && grabbedObject == null)
                {
                    grabbedObject = grabbaleObject;
                    parentContainer = grabbaleObject.GetComponentInParent<ParentContainer>();
                    grabby = grabbedObject.GetComponent<Grabbable>();
                    grabby.grabbed = true;
                    grabby.beltMovement = false;
                    grabby.onBelt = false;
                    GetComponent<Animator>().SetBool("on", true);
                    overGrabbable = false;
                }
                else if(grabbedObject!=null && overGrabbable == false)
                {
                    grabbedObject.GetComponent<Grabbable>().grabbed = false;
                    grabbedObject.GetComponent<Grabbable>().newY = transform.position.y;
                    grabbedObject.GetComponent<Grabbable>().checkTeleportation();
                    grabbedObject = null;
                    parentContainer = null;
                    GetComponent<Animator>().SetBool("on", false);
                    overGrabbable = true;
                }
                else if(grabbedObject != null && overGrabbable == true)
                {
                    //tell player bad
                }
                isMoving = false;
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabbable")
        {
            if (grabbedObject == null)
            {
                grabbaleObject = collision.gameObject;
            }
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
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Grabbable")
    //    {
    //        if (grabbedObject == null) grabbaleObject = collision.gameObject;
    //        overGrabbable = true;
    //    }
    //}
}
