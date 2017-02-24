using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : NetworkBehaviour {

    public int count = 0;
    public int observers = 0;
	// Use this for initialization
	void Start () {
        if(isServer)
        {
            Debug.Log("00000000000");
        }
        if(isClient)
        {
            Debug.Log("11111111111");
        }
    }
	
	// Update is called once per frame
	void Update () {
       
	}


    
}
