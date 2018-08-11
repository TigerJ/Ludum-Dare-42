using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wench : MonoBehaviour {

    public bool isMoving;
    public Vector3 targetPosition;
    public float speed = .01f;
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
        }
        if (Mathf.Abs(transform.position.y - targetPosition.y) < .04) isMoving = false;
    }
}
