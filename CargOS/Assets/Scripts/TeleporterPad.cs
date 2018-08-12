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
    Color green = new Color(.1062f, .7264f, .2569f);
    Color pink = new Color(.7254f, .1058f, .6557f);
    Color orange = new Color(1f, .3212f, .0f);
    Color blue = new Color(0f, .7070f, 1f);
    Color yellow = new Color(.8f, .8f, .08f);
    Color white = new Color(1f, 1f, 1f);
    public int type = 0;
    public AudioSource sfx_teleport;
    // Use this for initialization
    void Start () {
		
	}
    private void Awake()
    {
        ChangeColor();
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
            sfx_teleport.Play();
            type = Random.Range(0, 6);
            ChangeColor();
            teleporterState = 1;
            GetComponent<Animator>().SetInteger("state", teleporterState);
            ready = false;
            teleported = false;
            teleporterCooldown = teleporterSpeed;
            Destroy(cargo);
            GameObject.Find("Player").GetComponent<Player>().scorePoints();
        }

        if (cargo != null && ready == true) teleported = true;

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
            case 3:
                render.color = blue;
                break;
            case 4:
                render.color = yellow;
                break;
            case 5:
                render.color = white;
                break;
        }
    }
}
