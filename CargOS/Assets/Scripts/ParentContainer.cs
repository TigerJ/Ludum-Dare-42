using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentContainer : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Teleport()
    {
        Grabbable[] childContainers = GetComponentsInChildren<Grabbable>();
        int teleporterCount = 0;
        foreach (Grabbable g in childContainers)
        {
            if (g.onTeleporter == false)
            {
                teleporterCount++;
            }
        }
        if (teleporterCount == 0)
        {
            foreach (Grabbable g in childContainers) g.checkTeleportation();
        }
    }
}
