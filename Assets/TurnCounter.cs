using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : NetworkBehaviour {

    public int count = 0;
	// Use this for initialization
	void Start () {
       if(isClient)
        {
            Debug.Log("111111111111");
        }
       else if(isServer)
        {
            Debug.Log("000000000000");
        }
      //  OwnID = netId;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    
}
