using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : NetworkBehaviour {
    public bool ClientTrun = false;
    public int CurrentID=0;
    public int OwnID;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        OwnID = Network.connections.Length;
    }
	
	// Update is called once per frame
	void Update () {
	if(!isLocalPlayer)
        {
            return;
        }



	}
}
