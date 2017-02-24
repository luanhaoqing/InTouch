using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : NetworkBehaviour {
    [SyncVar]
    public int count = 0;
   
    public int OwnId;

	// Use this for initialization
	void Start () {
        count++;
        OwnId = count;
    }
	
	// Update is called once per frame
	void Update () {
     
	}


    
}
