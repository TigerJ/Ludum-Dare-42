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
    public bool overTeleporter;
    public TeleporterPad overPad;
    public string direction;
    public AudioSource item_drop;
    public AudioSource item_pickup;
    public AudioSource bad_player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            if (transform.position.y < targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.y);
            }
            else if (transform.position.y > targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.y);
            }
            if (grabbedObject != null) grabbedObject.transform.position = transform.position;
            if ((direction == "down" && transform.position.y >= targetPosition.y) || (direction == "up" && transform.position.y <= targetPosition.y))
            {
                Grabbable grabby;
                if (overGrabbable == true && grabbedObject == null)
                {
                    item_pickup.Play();
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
                    grabby = grabbedObject.GetComponent<Grabbable>();
                    if (overTeleporter == true)
                    {
                        if (grabby.type != overPad.type)
                        {
                            //tell the player bad
                            bad_player.Play();
                            isMoving = false;
                            return;
                        }
                    }
                    item_drop.Play();
                    grabby.grabbed = false;
                    grabby.newY = transform.position.y;
                    if(overTeleporter) grabby.checkTeleportation();
                    grabbedObject = null;
                    parentContainer = null;
                    GetComponent<Animator>().SetBool("on", false);
                    overGrabbable = true;
                }
                else if(grabbedObject != null && overGrabbable == true)
                {
                    //tell player bad
                    bad_player.Play();
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
        if (collision.gameObject.tag == "Teleporter")
        {
            overTeleporter = true;
            overPad = collision.gameObject.GetComponent<TeleporterPad>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabbable")
        {
            overGrabbable = false;
        }
        if (collision.gameObject.tag == "Teleporter")
        {
            overTeleporter = false;
            overPad = null;
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
