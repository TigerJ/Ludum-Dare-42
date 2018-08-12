using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {
    public bool takeTurn = false;
    public bool grabbed = false;
    public bool onBelt = false;
    public bool onTeleporter = false;
    public bool beltMovement = false;
    public float newY;
    public float parentOffsetX;
    public float parentOffsetY;
    GameObject teleporter;
    Color green = new Color(.1062f, .7264f, .2569f);
    Color pink = new Color(.7254f, .1058f, .6557f);
    Color orange = new Color(1f, .3212f, .0f);
    public int type = 0;
    // Use this for initialization
    void Start () {
        newY = transform.position.y;
        takeTurn = false;
        onTeleporter = false;
        beltMovement = false;
        grabbed = false;
	}
    private void Awake()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                render.color = green;
                break;
            case 1:
                render.color = pink;
                break;
            case 2:
                render.color = orange;
                break;
        }
    }
    // Update is called once per frame
    void Update () {
        if (takeTurn == true)
        {
            if (onBelt == true)
            {
                beltMovement = true;
                newY = transform.position.y - 1;
            }
            takeTurn = false;
        }

        if (beltMovement = true && transform.position.y > newY && grabbed == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);
        }
        else beltMovement = false;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Belt")
        {
            onBelt = true;
        }
        if(collision.gameObject.tag == "Teleporter")
        {
            onTeleporter = true;
            teleporter = collision.gameObject;
        }
    }
    public void activateGrabbable()
    {
        if (grabbed == false) takeTurn = true;
    }
    public void checkTeleportation()
    {
        if(onTeleporter == true)
        {
            teleporter.GetComponent<TeleporterPad>().cargo = gameObject;
        }
    }
    public void RollType()
    {
        type = Random.Range(0, 3);
        ChangeColor();
    }
    void ChangeColor()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                render.color = green;
                break;
            case 1:
                render.color = pink;
                break;
            case 2:
                render.color = orange;
                break;
        }
    }
}
