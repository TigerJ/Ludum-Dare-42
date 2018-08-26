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
    public GameObject light;
    public GameObject currentCommand;
    public ParentContainer parentContainer;
    public bool overTeleporter;
    bool overBelt;
    public TeleporterPad overPad;
    public string direction;
    public AudioSource item_drop;
    public AudioSource item_pickup;
    public AudioSource bad_player;
    Vector3 velo;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            Vector3 dampTar = new Vector3(transform.position.x, targetPosition.y, 0f);
            //transform.position = Vector3.SmoothDamp(transform.position, dampTar, ref velo, .05f);
            transform.position = Vector3.MoveTowards(transform.position, dampTar, .2f);
            //if (transform.position.y < targetPosition.y)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.y);
            //}
            //else if (transform.position.y > targetPosition.y)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.y);
            //}
            if (grabbedObject != null) grabbedObject.transform.position = transform.position;
            //Debug.Log(Vector3.Distance(transform.position, targetPosition));
            if (Vector3.Distance(transform.position,targetPosition) < .51)
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
                    light.GetComponent<Animator>().SetBool("on", true);
                    light.GetComponent<WinchLight>().type = grabby.type;
                    light.GetComponent<WinchLight>().changeColor();
                    overGrabbable = false;
                }
                else if (grabbedObject != null && overGrabbable == false)
                {
                    grabby = grabbedObject.GetComponent<Grabbable>();
                    if (overBelt == true)
                    {
                        Debug.Log("over belt");
                        //tell the player bad
                        bad_player.Play();
                        isMoving = false;
                        GetComponentInParent<Crane>().targets.RemoveAt(0);
                        GetComponentInParent<Crane>().checkTargets();
                        Destroy(currentCommand);
                        grabby.onBelt = false;
                        return;
                    }
                    if (overTeleporter == true)
                    {
                        if (grabby.type != overPad.type)
                        {
                            Debug.Log("type mismatch");
                            //tell the player bad
                            bad_player.Play();
                            isMoving = false;
                            GetComponentInParent<Crane>().targets.RemoveAt(0);
                            GetComponentInParent<Crane>().checkTargets();
                            Destroy(currentCommand);
                            return;
                        }
                    }
                    item_drop.Play();
                    grabby.onBelt = false;
                    grabby.grabbed = false;
                    grabby.newY = transform.position.y;
                    if (overTeleporter) grabby.checkTeleportation();
                    grabbedObject = null;
                    parentContainer = null;
                    GetComponent<Animator>().SetBool("on", false);
                    light.GetComponent<Animator>().SetBool("on", false);
                    overGrabbable = true;
                }
                else if (grabbedObject != null && overGrabbable == true)
                {
                    //tell player bad
                    bad_player.Play();
                    Debug.Log("over shipment");
                }
                isMoving = false;
                GetComponentInParent<Crane>().targets.RemoveAt(0);
                GetComponentInParent<Crane>().checkTargets();
                Destroy(currentCommand);
            }
            
            //if ((direction == "down" && transform.position.y >= targetPosition.y) || (direction == "up" && transform.position.y <= targetPosition.y))
            //{
                
            //}
            
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
            if (grabbedObject != null)
            {
                if (collision.gameObject.GetInstanceID() == grabbedObject.GetInstanceID())
                {
                    overGrabbable = false;
                }
            }
        }
        if (collision.gameObject.tag == "Teleporter")
        {
            overTeleporter = true;
            overPad = collision.gameObject.GetComponent<TeleporterPad>();
        }
        if(collision.gameObject.tag == "Belt")
        {
            overBelt = true;
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
        if (collision.gameObject.tag == "Belt")
        {
            overBelt = false;
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
