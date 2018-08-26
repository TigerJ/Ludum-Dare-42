using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchLight : MonoBehaviour {
    public int type;
    Color green = new Color(.1062f, .7264f, .2569f);
    Color pink = new Color(.7254f, .1058f, .6557f);
    Color orange = new Color(1f, .3212f, .0f);
    Color blue = new Color(0f, .7070f, 1f);
    Color yellow = new Color(.8f, .8f, .08f);
    Color white = new Color(1f, 1f, 1f);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changeColor()
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
