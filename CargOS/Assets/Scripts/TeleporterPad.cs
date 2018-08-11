using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterPad : MonoBehaviour {
    bool teleported = false;
    float teleporterSpeed = 0.5f;
    float teleporterCooldown = 0;
    int teleporterState = 0;
    bool ready = true;
    public GameObject cargo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (teleporterCooldown <= 0 && ready == false)
        {
            teleporterState++;
            if (teleporterState == 8)
            {
                teleporterState = 0;
                ready = true;
                if (cargo != null)
                {
                    teleported = true;
                }
            }
            GetComponent<Animator>().SetInteger("state", teleporterState);
            if (teleporterState != 0) teleporterCooldown = teleporterSpeed;
        }

        if (teleporterCooldown > 0) teleporterCooldown = teleporterCooldown - Time.deltaTime;

        if (teleported == true)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            teleporterState = 1;
            GetComponent<Animator>().SetInteger("state", teleporterState);
            ready = false;
            teleported = false;
            teleporterCooldown = teleporterSpeed;
            Destroy(cargo);
        }

        if (cargo != null && ready == true) teleported = true;

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
