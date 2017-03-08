using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerIDOnBoard : NetworkBehaviour {
    public int PlayerIDOB;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;
        PlayerIDOB = GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
