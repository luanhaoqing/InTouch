using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : NetworkBehaviour {

    public int count = 0;
	// Use this for initialization
	void Start () {
      
        count++;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    
}
