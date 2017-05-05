using UnityEngine;
using System.Collections;

public class GateShow : MonoBehaviour {
    public GameObject door;
    public int count=0;
    public GameObject enddetecter;
    public GameObject cursor;
	// Use this for initialization
	void Start () {
        door.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
        if(count==2)
        {
            door.SetActive(true);
            enddetecter.SetActive(true);
            //this.enabled = false;
        }
	}
}
