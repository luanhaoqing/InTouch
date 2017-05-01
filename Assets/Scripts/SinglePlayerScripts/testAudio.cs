using UnityEngine;
using System.Collections;

public class testAudio : MonoBehaviour {

    public bool test = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (test){
            AudioCenter.PlayCantDoThat();
            test = !test;
        }
	
	}
}
