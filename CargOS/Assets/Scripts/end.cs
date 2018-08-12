using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class end : MonoBehaviour {
    public Text finalScore;
	// Use this for initialization
	void Start () {
		
	}
    private void Awake()
    {
         finalScore.text = "your score was: " + GameObject.Find("ScoreHolder").GetComponent<LastScore>().score.ToString("D7");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
